using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RagdollOn()
    {
        _animator.enabled = false;
        KinematicToggle(false);
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
