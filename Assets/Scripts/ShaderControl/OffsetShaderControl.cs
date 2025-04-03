using UnityEngine;

public class OffsetShaderControl : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Vector2 _targetSpeed;
    private static readonly int MainTexSpeedID = Shader.PropertyToID("MainTexSpeed_");

    public void StartMove()
    {
        _material.SetVector(MainTexSpeedID, _targetSpeed);
    }

    public void StopMove()
    {
        _material.SetVector(MainTexSpeedID, Vector2.zero);
    }
}
