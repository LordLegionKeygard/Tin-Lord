using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "TinLord/Building")]
public class Building : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite BuildingSprite;
    public int Id;
    public int BuildingLevel;
    public int RequiredBaseLevel; // требуемый уровень базы для постройки этого здания
    public float BuildingEcology;
    public float BuildingHealth;
    public float ResourceExtractedAmount; // кол-во создаваемого ресурса за 1 тик времени
    public float Price; // цена покупки за фрагменты памяти
    public bool CanRotateBuilding;

    [Header("Requires")]
    public ResourceWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства здания
    public ResourcesForWorkWrapper[] ResourcesForWork; // кол-во ресурсов для работы здания
    public ResourcesProductionWrapper[] ResourcesProduction; // кол-во ресурсов которые может создавать здание

    [Header("VFX")]
    public GameObject DestroyVFXPrefab;
    public GameObject ConstructionPrefab;

    [Header("Turret")]
    public float Damage;
    public float AttackSpeed;
    public float AttackRadius;
    public float RotationSpeed;
    public float KnockbackPoints;

    [Header("EcologyBuilding")]
    public int BuildingEcologyPurifier; // кол-во очков экологии которое дает здание по очистке экологии

    [Header("RandomTransformOnTile")]
    [Range(0f, 2.5f)] public float RandomRange;
    public bool IsChangePosition;
    public bool IsChangeRotation;
    public bool IsFixed90Rotation;
}

[System.Serializable]
public class ResourceWrapper
{
    public ResourceEnum ResourceEnum;
    public int RecourceAmount;
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