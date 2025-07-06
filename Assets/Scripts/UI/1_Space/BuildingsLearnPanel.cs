using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsLearnPanel : MonoBehaviour
{
    [SerializeField] private LearnBuildingItem[] _learnBuildingItems;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Resource[] _resources;
    private int _baseBuildingsCount = 4;

    // Список всех ресурсов, которые игрок уже может производить
    private readonly HashSet<ResourceEnum> _unlockedResources = new();

    public LearnBuildingItem[] AllLearnBuildingItems() => _learnBuildingItems;
    public bool IsResourceUnlocked(ResourceEnum res) => _unlockedResources.Contains(res);

    public int GetCurrentBaseLevel()
    {
        int level = 0;
        for (int i = 0; i < _baseBuildingsCount && i < _learnBuildingItems.Length; i++)
        {
            if (_learnBuildingItems[i].IsLearn())
            {
                level = Mathf.Max(level, _learnBuildingItems[i].GetBuilding().BuildingLevel); // 1,2,3,4
            }
        }
        return level;
    }

    public LearnBuildingItem GetBaseItemByLevel(int requiredLevel)
    {
        for (int i = 0; i < _baseBuildingsCount; i++)
            if (_learnBuildingItems[i].GetBuilding().BuildingLevel == requiredLevel)
                return _learnBuildingItems[i];
        return null;
    }


    public void ResetScrollPosition()
    {
        _scrollRect.verticalNormalizedPosition = 1f;
    }

    // Добавляем в трекер ресурсы, которые производит здание
    public void RegisterBuilding(Building building)
    {
        foreach (var prod in building.ResourcesProduction)
            _unlockedResources.Add(prod.ProductionResource.ResourceEnum);

        RefreshAllItems();
    }


    private bool ResourceSatisfied(ResourceEnum res)
    {
        return _unlockedResources.Contains(res);
    }

    private void RefreshAllItems()
    {
        foreach (var item in _learnBuildingItems)
        {
            item.RefreshView();
        }
    }

    public bool TryGetBlockingResource(Building b, out ResourceEnum missing)
    {
        missing = ResourceEnum.None;

        foreach (var need in b.ResourcesForBuild)
        {
            if (!ResourceSatisfied(need.ResourceEnum))
            {
                missing = need.ResourceEnum; return false;
            }
        }

        if (b.ResourcesForWork.Length > 0)
        {
            bool fuelOk = false;
            ResourceEnum fail = ResourceEnum.None;

            foreach (var rw in b.ResourcesForWork)
            {
                if (ResourceSatisfied(rw.ResourceForWork.ResourceEnum)) fuelOk = true;
                else if (fail == ResourceEnum.None) fail = rw.ResourceForWork.ResourceEnum;
            }
            if (!fuelOk)
            {
                missing = fail; return false;
            }
        }

        if (b.ResourcesProduction.Length == 0) return true;

        ResourceEnum firstFail = ResourceEnum.None;

        foreach (var prod in b.ResourcesProduction)
        {
            bool recipeOk = true;

            foreach (var rec in prod.ResourceRecept)
            {
                if (!ResourceSatisfied(rec.ResourceForRecept.ResourceEnum))
                {
                    recipeOk = false;
                    if (firstFail == ResourceEnum.None)
                        firstFail = rec.ResourceForRecept.ResourceEnum;
                }
            }

            if (recipeOk) return true;
        }

        missing = firstFail;
        return false;
    }

    // Первый (по порядку массива) элемент, который производит указанный ресурс
    public LearnBuildingItem GetProducerOf(ResourceEnum res)
    {
        foreach (var item in _learnBuildingItems)
        {
            var b = item.GetBuilding();
            foreach (var prod in b.ResourcesProduction)
            {
                if (prod.ProductionResource.ResourceEnum == res)
                {
                    return item;
                }
            }
        }
        return null;
    }

    // Возвращает локализованное название типа (категории) здания, которому принадлежит item.
    public string GetParentTileName(LearnBuildingItem item, Tile[] allBuildingTypes)
    {
        // Сам LearnBuildingItem не знает про «тип», но вы легко проходите
        // по тем же  _allBuildingTypes  в LearnBuildingInfoPanel.
        // Делаем то же самое, только один раз и централизованно.

        Building target = item.GetBuilding();

        foreach (var tile in allBuildingTypes)
        {
            if (tile == null || tile.Buildings == null) continue;

            foreach (var b in tile.Buildings)
                if (b == target)
                    return tile.Name[Language.LanguageNumber];
        }
        return string.Empty;
    }

}
