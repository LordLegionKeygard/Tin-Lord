using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "TinLord/Building")]
public class Building : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite BuildingSprite;
    public int BuildingEcology;
    public float ResourceExtractedAmount; // кол-во создаваемого ресурса за 1 тик времени

    [Header("Requires")]
    public ResourcesForBuildWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства здания
    public ResourcesForWorkWrapper[] ResourcesForWork; // кол-во ресурсов для работы здания
    public ResourcesProductionWrapper[] ResourcesProduction; // кол-во ресурсов которые может создавать здание
}

[System.Serializable]
public class ResourcesForBuildWrapper
{
    public ResourceEnum ResourcesForBuild;
    public int RecourcesForBuildAmount;
}

[System.Serializable]
public class ResourcesForWorkWrapper
{
    public Resource ResourceForWork; // ресурс необходимый для работы здания
    public float ResourcesForWorkAmount; // кол-во ресурса для работы
}

[System.Serializable]
public class ResourcesProductionWrapper
{
    public Resource ProductionResource; // добываемый или создаваемый зданием ресурс
    public ResourceRecept[] ResourceRecept; // рецепт для создания ресурса
}

[System.Serializable]
public class ResourceRecept
{
    public Resource ResourceForRecept; // ресурс для создания предмета
    public float ResourcesForReceptAmount; // кол-во ресурса для создания предмета
}