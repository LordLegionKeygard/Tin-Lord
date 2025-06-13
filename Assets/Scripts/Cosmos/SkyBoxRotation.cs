using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{
    [SerializeField] private Material _skyBoxMat;

    private float _value;

    private void LateUpdate()
    {
        _value += 0.1f * Time.deltaTime;
        _skyBoxMat.SetFloat("_Rotation", _value);
    }

    private void OnDisable()
    {
        _skyBoxMat.SetFloat("_Rotation", 0);
    }
}
