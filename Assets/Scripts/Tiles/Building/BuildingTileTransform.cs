using UnityEngine;

public class BuildingTileTransform : MonoBehaviour
{
    [Header("CachedTransform")]
    private float _positionX;
    private float _positionY;
    private float _positionZ;

    public float GetPositionX() => _positionX;
    public float GetPositionY() => _positionY;
    public float GetPositionZ() => _positionZ;

    public void CachedRandomTransform(Building building)
    {
        _positionX = Random.Range(-building.RandomRange, building.RandomRange);
        _positionZ = Random.Range(-building.RandomRange, building.RandomRange);

        var rnd = Random.Range(0, 4); //для фиксированного вращения
        var rndRot = Random.Range(0, 360);

        _positionY = building.IsFixed90Rotation ? rnd * 90 : rndRot;
    }

    public void LoadTransform(BuildingData buildingData)
    {
        _positionX = buildingData.BuildingTilePositionX;
        _positionY = buildingData.BuildingTilePositionY;
        _positionZ = buildingData.BuildingTilePositionZ;
    }

    public void SetTransform(Transform objectTransform, Building building, TileObject tileObject)
    {
        if (building.IsChangePosition)
        {
            objectTransform.localPosition += new Vector3(_positionX, 0, _positionZ);
        }

        if (building.IsChangeRotation)
        {
            objectTransform.localRotation = Quaternion.Euler(objectTransform.rotation.x, _positionY, objectTransform.rotation.z);
        }

        SetUniqueTransform(objectTransform, tileObject);
    }


    /// <summary>
    /// Вращает обьект на ту же позицию, что и тайл земли. У тайла земли при этом на model должен быть скрипт SetTileRotation и указана цифра родителя для получения компонента.
    /// На данный момент нужна только для горы и моста. В остальных случаях отдельно класть RandomTileRotation
    /// </summary>
    private void SetUniqueTransform(Transform objectTransform, TileObject tileObject)
    {
        if (tileObject.BuildingTileObject().GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Bridge)
        {
            objectTransform.localRotation = Quaternion.Euler(0, tileObject.GroundTileObject().GroundModelRotation(), 0);
        }

        if (tileObject.BuildingTileObject().GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.StoneMining)
        {
            objectTransform.localRotation = Quaternion.Euler(0, tileObject.GroundTileObject().GroundModelRotation()
+ tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>().transform.eulerAngles.y, 0);
        }
    }
}
