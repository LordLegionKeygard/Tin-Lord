using UnityEngine;

public class MachineWheelsRotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.right;
    [SerializeField] private WheelWrapper[] wheels;
    private float[] _angles;
    private bool _isRotating;

    private void Awake()
    {
        _angles = new float[wheels.Length];
    }

    private void FixedUpdate()
    {
        if (!_isRotating) return;

        for (int i = 0; i < wheels.Length; i++)
        {
            _angles[i] += Time.deltaTime * wheels[i].RotateSpeed;
            if (_angles[i] > 360.0f) _angles[i] -= 0;

            wheels[i].Wheel.localRotation = Quaternion.Euler(rotationAxis * _angles[i]);
        }
    }

    public void StartRotate() => _isRotating = true;
    public void StopRotate()  => _isRotating = false;
}

[System.Serializable]
public class WheelWrapper
{
    public Transform Wheel;
    public float RotateSpeed;
}
