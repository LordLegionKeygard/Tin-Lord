using UnityEngine;

public class RobotRiffleTransformChanger : MonoBehaviour
{
    [SerializeField] private Transform _riffle;
    public void ChangeTransform(string isRange)
    {
        var isRangeTransform = isRange == "true"; 
        _riffle.transform.localPosition = isRangeTransform ? new Vector3(-0.1865386f, 0.6207381f, -0.07245889f) : new Vector3(0.402f, 0.19f, -0.034f);
        _riffle.transform.localRotation = isRangeTransform ? Quaternion.Euler(60.342f, -275.416f, -3.176f) : Quaternion.Euler(4.048f, 267.455f, -178.425f);
    }
}
