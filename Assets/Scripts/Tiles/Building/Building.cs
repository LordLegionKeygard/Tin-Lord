using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "TinLord/Building")]
public class Building : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite BuildingSprite;
    public int BuildingEcology;
    public float ResourceExtractedAmount; // за 1 тик времени

    [Header("Requires")]
    public ResourcesForBuildWrapper[] ResourcesForBuild;
    public ResourcesForWorkWrapper[] ResourcesForWork;
}
