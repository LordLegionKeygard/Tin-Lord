using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float Speed() => _speed;
    [SerializeField] private bool _canMove = true;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void CanRun()
    {
        if (!_canMove) return;
        // _aiPath.maxSpeed = _runSpeed;
    }

    public void CantMove()
    {
        _canMove = false;
        // _aiPath.maxSpeed = 0;
    }
    public void CanMove()
    {
        _canMove = true;
    }
}
