using System.Collections;
using UnityEngine;

public class SkillColliderDestroyTimer : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private bool _needActivate;
    [SerializeField] private bool _needDeactivate;
    [SerializeField] private bool _needDestroy;
    [SerializeField] private float _activateTime;
    [SerializeField] private float _deactivateTime;
    [SerializeField] private float _destroyTime;


    private void Start()
    {
        if(_needActivate) StartCoroutine(ColliderActivate());
        if(_needDeactivate) StartCoroutine(ColliderDeactivate());
        if(_needDestroy) Destroy(gameObject, _destroyTime);
    }

    private IEnumerator ColliderActivate()
    {
        yield return new WaitForSeconds(_activateTime);
        _collider.enabled = true;
    }

    private IEnumerator ColliderDeactivate()
    {
        yield return new WaitForSeconds(_deactivateTime);
        _collider.enabled = false;
    }
}
