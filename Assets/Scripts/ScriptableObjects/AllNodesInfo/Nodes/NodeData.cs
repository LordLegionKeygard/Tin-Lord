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
    public Texture PlanetTexture;
    public Vector3 PlanetPosition;
    public Vector3 PlanetRotation;
    public Material CosmosSkybox;
    public float SkyboxRotation;

    public Vector3 LightRotation;
    public float Temperature;
}
