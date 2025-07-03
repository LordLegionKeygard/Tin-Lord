using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsLearnPanel : MonoBehaviour
{
    [SerializeField] private LearnBuildingItem[] _learnBuildingItems;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Resource[] _resources;
    private int _baseBuildingsCount = 4;

    /// <summary>Список всех ресурсов, которые игрок уже может производить.</summary>
    private readonly HashSet<ResourceEnum> _unlockedResources = new();

    public LearnBuildingItem[] AllLearnBuildingItems() => _learnBuildingItems;
    public bool IsResourceUnlocked(ResourceEnum res) => _unlockedResources.Contains(res);

    public int GetCurrentBaseLevel()
    {
        int level = 0;
        for (int i = 0; i < _baseBuildingsCount && i < _learnBuildingItems.Length; i++)
            if (_learnBuildingItems[i].IsLearn())
                level = Mathf.Max(level, _learnBuildingItems[i].GetBuilding().BuildingLevel); // 1,2,3,4
        return level;                 // 0 если не выучено ни одного
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

    /// <summary>Добавляем в трекер ресурсы, которые производит здание.</summary>
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

        /* ---------- 1. строительство (AND) ---------- */
        foreach (var need in b.ResourcesForBuild)
            if (!ResourceSatisfied(need.ResourceEnum))
            { missing = need.ResourceEnum; return false; }

        /* ---------- 2. топливо (OR) ---------- */
        if (b.ResourcesForWork.Length > 0)
        {
            bool fuelOk = false;
            ResourceEnum fail = ResourceEnum.None;

            foreach (var rw in b.ResourcesForWork)
            {
                if (ResourceSatisfied(rw.ResourceForWork.ResourceEnum)) fuelOk = true;
                else if (fail == ResourceEnum.None) fail = rw.ResourceForWork.ResourceEnum;
            }
            if (!fuelOk) { missing = fail; return false; }
        }

        /* ---------- 3. рецепты (ANY recipe) ---------- */
        if (b.ResourcesProduction.Length == 0) return true;   // нет рецептов – всё ок

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
                        firstFail = rec.ResourceForRecept.ResourceEnum;  // запомним первый недостающий
                }
            }

            if (recipeOk) return true;   // хотя бы один рецепт полностью доступен
        }

        // ни один рецепт не прошёл
        missing = firstFail;
        return false;
    }

}
