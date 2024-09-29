using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class CreatureSpeed : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private bool _canMove = true;
    private AIPath _aiPath;
    private Animator _animator;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _animator = GetComponent<Animator>();
    }

    public void CanRun()
    {
        if (!_canMove) return;
        _aiPath.maxSpeed = _runSpeed;
    }

    public void CantMove()
    {
        _canMove = false;
        _aiPath.maxSpeed = 0;
    }
    public void CanMove()
    {
        _canMove = true;
    }
}
