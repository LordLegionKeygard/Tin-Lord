using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainView : MonoBehaviour
{
    [Header("Terrain")]
    [SerializeField] private TerrainObjectsWrapper[] _terrainObjects;

    public void PrepareTerrain()
    {
        var mission = CurrentMissionInfo.Instance.GetCurrentMission();

        foreach (var item in _terrainObjects[mission.MissionId].TerrainObjects)
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
