using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вращает обьект на ту же позицию, что и тайл земли. У тайла земли при этом на model должен быть скрипт SetTileRotation и указана цифра родителя для получения компонента.
/// На данный момент нужна только для горы и моста. В остальных случаях отдельно класть RandomTileRotation
/// </summary>
public class BuildingRotation : MonoBehaviour
{
    [SerializeField] private BuildingLevels _buildingLevels;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, _buildingLevels.CurrentTileObject().GroundTileObject().GroundModelRotation(), 0);
    }
}
