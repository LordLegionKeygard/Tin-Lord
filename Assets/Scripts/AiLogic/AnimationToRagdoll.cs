using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] private GameObject _activeObject;
    [SerializeField] private GameObject[] _disableObjects;
    [SerializeField] private Rigidbody[] _rigidbodies;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActiveRagdoll()
    {
        if (_activeObject!= null)
        {
            foreach (var item in _disableObjects)
            {
                item.SetActive(false);
            }
            _activeObject.SetActive(true);
        }
        else
        {
            _animator.enabled = false;
            KinematicToggle(false);
        }
    }


    public void KinematicToggle(bool state)
    {
        foreach (var rb in _rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = state;
        }
    }
}
