using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "TinLord/Nodes/NodeData")]
public class NodeData : ScriptableObject
{
    public NodeType NodeType;
    public Sprite Icon;
    public Color IconColor;
    public int IconWidth;
    public int IconHeight;
    public int DescriptionTextNumber;
    public CosmosVariations[] CosmosVariations;
}

[System.Serializable]
public class CosmosVariations
{
    [Header("Planet")]
    public GameObject PlanetPrefab;
    public Vector3 PlanetPosition;
    public Vector3 PlanetRotation;

    [Header("Cosmos")]
    public Material CosmosSkybox;
    public float SkyboxRotation;

    [Header("Light")]
    public Vector3 LightRotation;
    public float Temperature;

    [Header("Environment")]
    public GameObject EnvironmentPrefab;
    public Vector3 EnvironmentPosition;
    public Vector3 EnvironmentRotation;
}
