using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вращает обьект на ту же позицию, что и тайл земли. У тайла земли при этом на model должен быть скрипт RandomTileRotation. А так же стоять чекбокс и указана цифра родителя для получения компонента.
/// </summary>
public class BuildingRotation : MonoBehaviour
{
    [SerializeField] private BuildingLevels _buildingLevels;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, _buildingLevels.CurrentTileObject().GroundTileObject().GroundModelRotation(), 0);
    }
}
