using UnityEngine;

public class MachineTakeDamageVFX : BaseTakeDamageVFX
{
    private CapsuleCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Height = _collider.height;
    }
}
