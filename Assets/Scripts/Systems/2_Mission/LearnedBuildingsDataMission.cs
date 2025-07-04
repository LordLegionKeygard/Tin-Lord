using UnityEngine;

public class LearnedBuildingsDataMission : MonoBehaviour
{
    [SerializeField] private bool[] _buildingsLearned;

    public bool IsBuildingLearned(int id) => _buildingsLearned[id];

    public void LoadLearnedBuildings(bool[] bools)
    {
        _buildingsLearned = bools;
    }

    public bool IsHaveOneLearnedBuildingInBuildingType(Tile buildingTypeTile)
    {
        for (int i = 0; i < buildingTypeTile.Buildings.Length; i++)
        {
            if (IsBuildingLearned(buildingTypeTile.Buildings[i].Id)) return true;
        }

        return false;
    }

    public bool IsHaveLearnedBuildingUpgradeInBuildingType(Tile buildingTypeTile, int currentLevel)
    {
        for (int i = currentLevel; i < buildingTypeTile.Buildings.Length; i++)
        {
            if (IsBuildingLearned(buildingTypeTile.Buildings[i].Id)) return true;
        }

        return false;
    }
}
