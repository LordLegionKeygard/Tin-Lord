using UnityEngine;

public class TerrainView : MonoBehaviour
{
    [SerializeField] private TerrainObjectsWrapper[] _terrainObjects;

    public void PrepareTerrain()
    {
        var landscape = CurrentMissionInfo.Instance.GetCurrentLandscape();

        foreach (var item in _terrainObjects[(int)landscape.LandscapeEnum].TerrainObjects)
        {
            item.SetActive(true);
        }
        
    }
}

[System.Serializable]
public class TerrainObjectsWrapper
{
    public GameObject[] TerrainObjects;
}
