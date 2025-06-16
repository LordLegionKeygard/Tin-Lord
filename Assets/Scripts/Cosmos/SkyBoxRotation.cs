using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{
    private float _rotationSpeed = 0.05f;
    private Material _currentMat;
    private float _accum;

    private void LateUpdate()
    {
        var activeMat = RenderSettings.skybox;
        
        if (activeMat == null) return;

        if (activeMat != _currentMat)
        {
            _currentMat = activeMat;
            _accum = _currentMat.GetFloat("_Rotation");
        }

        _accum += _rotationSpeed * Time.deltaTime;

        if (_accum >= 360f) _accum -= 360f;

        _currentMat.SetFloat("_Rotation", _accum);
    }
}
