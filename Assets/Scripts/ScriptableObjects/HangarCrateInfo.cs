using UnityEngine;

[CreateAssetMenu(fileName = "HangarCrateInfo", menuName = "TinLord/Info/HangarCrateInfo")]
public class HangarCrateInfo : ScriptableObject
{
    public int Name;
    public Sprite CrateSprite;
    public int Price; // нейро осколки
    public HangarCrateType HangarCrateType;
    public ResourceWrapper[] ResourceWrapper;
}

[System.Serializable]
public enum HangarCrateType
{
    None = -1,
    BaseCrate = 0, // базовый ящик
    MetalCrate = 1, // метал контейнер
    SupplyCrate = 2, // Контейнер снабжения

    // Engineering Case Инженерный кейс
    // Assembly Line Crate Ящик сборочной линии
    // Tech Container Техноконтейнер
    // The Legacy of the Creators Наследие создателей

}
