using UnityEngine;

public class BuildingTileTransform : MonoBehaviour
{
    [Header("CachedTransform")]
    private float _randomPosX;
    private float _randomPosZ;
    private int _newRotation;

    public void CachedRandomTransform(Building building)
    {
        _randomPosX = Random.Range(-building.RandomRange, building.RandomRange);
        _randomPosZ = Random.Range(-building.RandomRange, building.RandomRange);

        var rnd = Random.Range(0, 4); //для фиксированного вращения
        var rndRot = Random.Range(0, 360);

        _newRotation = building.IsFixed90Rotation ? rnd * 90 : rndRot;
    }

    public void SetCachedTransform(Transform objectTransform, Building building, TileObject tileObject)
    {
        if (building.IsChangePosition)
        {
            objectTransform.localPosition += new Vector3(_randomPosX, 0, _randomPosZ);
        }

        if (building.IsChangeRotation)
        {
            objectTransform.localRotation = Quaternion.Euler(objectTransform.rotation.x, _newRotation, objectTransform.rotation.z);
        }

/// <summary>
/// Вращает обьект на ту же позицию, что и тайл земли. У тайла земли при этом на model должен быть скрипт SetTileRotation и указана цифра родителя для получения компонента.
/// На данный момент нужна только для горы и моста. В остальных случаях отдельно класть RandomTileRotation
/// </summary>
        if (tileObject.BuildingTileObject().CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Bridge)
        {
            objectTransform.localRotation = Quaternion.Euler(0, tileObject.GroundTileObject().GroundModelRotation(), 0);
        }

        if (tileObject.BuildingTileObject().CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.StoneMining)
        {
            objectTransform.localRotation = Quaternion.Euler(0, tileObject.GroundTileObject().GroundModelRotation()
+ tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>().transform.eulerAngles.y, 0);
        }
    }
}
