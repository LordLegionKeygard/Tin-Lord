using UnityEngine;

[CreateAssetMenu(fileName = "HangarDroneInfo", menuName = "TinLord/Info/HangarDroneInfo")]
public class HangarDroneInfo : ScriptableObject
{
    public int Name;
    public Sprite DroneSprite;
    public int Price; // нейро осколки
    public HangarDroneType HangarDroneType;
}

[System.Serializable]
public enum HangarDroneType
{
    None = -1,
    Scout = 0,
    Engineer = 1,
    Combat = 2,

}
