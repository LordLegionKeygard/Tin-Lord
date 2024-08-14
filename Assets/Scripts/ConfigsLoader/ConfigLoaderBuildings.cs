using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config", menuName = "TinLord/Configs/Buildings")]
public class ConfigLoaderBuildings : ScriptableObject
{
    [SerializeField] private Building[] _allBuildings;
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

        for (int i = 0; i < _allBuildings.Length; i++)
        {
            if (i >= _configs.Count)
            {
                Debug.Log($"Config index {i} out of bounds.");
                break;
            }

            BuildingConfigs config = _configs[i];
            _allBuildings[i].Name = new[] { config.EnglishName, config.RussianName };
            _allBuildings[i].BuildingEcology = config.BuildingEcology;
            _allBuildings[i].ResourceExtractedAmount = ParseFloat(config.ResourceExtractedAmount);
            _allBuildings[i].ResourcesForBuild = ParseResources(config.ResourcesForBuild);
            _allBuildings[i].ResourcesForWork = ParseResourcesForWork(config.ResourcesForWork);
            _allBuildings[i].ResourceCreate = ParseExtractedResources(config.ExtractedResources, config.ResourceRecept);

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_allBuildings[i]);
#endif        
        }
    }

    // Метод для парсинга ExtractedResources и заполнения ResourceCreate
    private ResourcesCreateWrapper[] ParseExtractedResources(string extractedResources, string resourceRecepts)
    {
        if (string.IsNullOrWhiteSpace(extractedResources))
        {
            Debug.LogWarning("ExtractedResources string is null or empty.");
            return new ResourcesCreateWrapper[0];
        }

        string[] resourceParts = extractedResources.Split('/');
        string[] receptParts = resourceRecepts?.Split('&') ?? new string[0];
        List<ResourcesCreateWrapper> resourceCreateList = new List<ResourcesCreateWrapper>();

        for (int i = 0; i < resourceParts.Length; i++)
        {
            if (int.TryParse(resourceParts[i], out int resourceIndex))
            {
                if (resourceIndex >= 0 && resourceIndex < _allResources.Length)
                {
                    var resourceCreate = new ResourcesCreateWrapper
                    {
                        CreateResource = _allResources[resourceIndex],
                        ResourceRecept = i < receptParts.Length 
                            ? ParseResourceRecept(receptParts[i]) 
                            : new ResourceRecept[0]
                    };

                    resourceCreateList.Add(resourceCreate);
                }
                else
                {
                    Debug.LogError($"Resource index {resourceIndex} is out of bounds.");
                }
            }
            else
            {
                Debug.LogError($"Invalid format for resource index: {resourceParts[i]}");
            }
        }

        return resourceCreateList.ToArray();
    }

    // Метод для парсинга строки ResourceRecept
    private ResourceRecept[] ParseResourceRecept(string recepts)
    {
        if (string.IsNullOrWhiteSpace(recepts))
        {
            Debug.LogWarning("ResourceRecept string is null or empty.");
            return new ResourceRecept[0];
        }

        string[] parts = recepts.Split('/');
        List<ResourceRecept> resourceReceptList = new List<ResourceRecept>();

        foreach (string part in parts)
        {
            string[] resourceAmount = part.Split(':');
            if (resourceAmount.Length == 2 &&
                int.TryParse(resourceAmount[0], out int resourceIndex) &&
                float.TryParse(resourceAmount[1], out float amount))
            {
                if (resourceIndex >= 0 && resourceIndex < _allResources.Length)
                {
                    resourceReceptList.Add(new ResourceRecept
                    {
                        ResourceForRecept = _allResources[resourceIndex],
                        ResourcesForReceptAmount = amount
                    });
                }
                else
                {
                    Debug.LogError($"Resource index {resourceIndex} is out of bounds.");
                }
            }
            else
            {
                Debug.LogError($"Invalid format for resource recept data: {part}");
            }
        }

        return resourceReceptList.ToArray();
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
        if (string.IsNullOrWhiteSpace(resources))
        {
            return new ResourcesForWorkWrapper[0];
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
    public string ExtractedResources; // Значения для создаваемых ресурсов
    public string ResourceRecept; // Новая переменная для хранения рецептов ресурсов
}
