using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config", menuName = "TinLord/Configs/Buildings")]
public class ConfigLoaderBuildings : ScriptableObject
{
    [SerializeField] private Building[] _allBuilding;
    [SerializeField] private Resource[] _allResources;
    private List<BuildingConfigs> _configs;

#if UNITY_EDITOR
    public void Load()
    {
#pragma warning disable CS0612 // Type or member is obsolete
        ReadGoogleSheets.FillData<BuildingConfigs>(WorldGameInfo.GoogleSheetId, WorldGameInfo.BuildingGridId, list =>
        {
            _configs = list;
            ReadGoogleSheets.SetDirty(this);
            SetConfigs();
        });
#pragma warning restore CS0612 // Type or member is obsolete
    }
#endif

    private void SetConfigs()
    {
        if (_configs == null || _configs.Count == 0)
        {
            Debug.Log("No data loaded or _configs list is empty.");
            return;
        }

        Debug.Log("Total configs: " + _configs.Count);

        // Loop through all buildings and configure them
        for (int i = 0; i < _allBuilding.Length; i++)
        {
            if (i >= _configs.Count)
            {
                Debug.Log($"Config index {i} out of bounds.");
                break;
            }

            BuildingConfigs config = _configs[i];
            _allBuilding[i].Name = new[] { config.EnglishName, config.RussianName };
            _allBuilding[i].BuildingEcology = config.BuildingEcology;
            _allBuilding[i].ResourceExtractedAmount = ParseFloat(config.ResourceExtractedAmount);
            _allBuilding[i].ResourcesForBuild = ParseResources(config.ResourcesForBuild);
            _allBuilding[i].ResourcesForWork = ParseResourcesForWork(config.ResourcesForWork);
        }
    }

    private float ParseFloat(string value)
    {
        return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result)
            ? result
            : 0f;
    }

    private ResourcesForBuildWrapper[] ParseResources(string resources)
    {
        string[] parts = resources.Split('/');
        List<ResourcesForBuildWrapper> buildWrapperList = new List<ResourcesForBuildWrapper>();

        foreach (string part in parts)
        {
            string[] resourceAmount = part.Split(':');
            if (resourceAmount.Length == 2 &&
                int.TryParse(resourceAmount[0], out int resourceValue) &&
                int.TryParse(resourceAmount[1], out int amount))
            {
                if (System.Enum.IsDefined(typeof(ResourceEnum), resourceValue))
                {
                    buildWrapperList.Add(new ResourcesForBuildWrapper
                    {
                        ResourcesForBuild = (ResourceEnum)resourceValue,
                        RecourcesForBuildAmount = amount
                    });
                }
                else
                {
                    Debug.LogError($"Unknown enum value: {resourceValue}");
                }
            }
            else
            {
                Debug.LogError($"Invalid format for resource data: {part}");
            }
        }

        return buildWrapperList.ToArray();
    }

    private ResourcesForWorkWrapper[] ParseResourcesForWork(string resources)
    {
        // Проверка на пустую строку
        if (string.IsNullOrWhiteSpace(resources))
        {
            return new ResourcesForWorkWrapper[0]; // Возвращаем пустой массив
        }

        string[] parts = resources.Split('/');
        List<ResourcesForWorkWrapper> workWrapperList = new List<ResourcesForWorkWrapper>();

        foreach (string part in parts)
        {
            string[] resourceAmount = part.Split(':');
            if (resourceAmount.Length == 2 &&
                int.TryParse(resourceAmount[0], out int resourceIndex) &&
                int.TryParse(resourceAmount[1], out int amount))
            {
                if (resourceIndex >= 0 && resourceIndex < _allResources.Length)
                {
                    workWrapperList.Add(new ResourcesForWorkWrapper
                    {
                        ResourceForWork = _allResources[resourceIndex],
                        ResourcesForWorkAmount = amount
                    });
                }
                else
                {
                    Debug.LogError($"Resource index {resourceIndex} is out of bounds.");
                }
            }
            else
            {
                Debug.LogError($"Invalid format for resource data: {part}");
            }
        }

        return workWrapperList.ToArray();
    }
}

[System.Serializable]
public class BuildingConfigs
{
    public string EnglishName;
    public string RussianName;
    public int BuildingEcology;
    public string ResourceExtractedAmount;
    public string ResourcesForBuild;
    public string ResourcesForWork;
}
