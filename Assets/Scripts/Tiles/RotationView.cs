using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Содержит список обьектов которые буду вращаться при нажатии на кнопку вращения в SelectTilePanel
/// </summary>
public class RotationView : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsForRotate;
    private float rotationAngle = 90f;
    private Vector3 rotationAxis = Vector3.up;

    public float GetObjectRotation() => _objectsForRotate[0].transform.localEulerAngles.y;

    public void Rotate()
    {
        for (int i = 0; i < _objectsForRotate.Length; i++)
        {
            _objectsForRotate[i].transform.Rotate(rotationAxis, rotationAngle);
        }
    }

    public void LoadRotate(float value)
    {
        for (int i = 0; i < _objectsForRotate.Length; i++)
        {
            _objectsForRotate[i].transform.localRotation = Quaternion.Euler(_objectsForRotate[i].transform.rotation.x, value, _objectsForRotate[i].transform.rotation.z);
        }
    }
}
