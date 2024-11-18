using UnityEngine;

public class RobotSpeed : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed = 1.5f;
    [SerializeField] private float _currentSpeed;
    public float Speed() => _currentSpeed;
    [SerializeField] private bool _canMove = true;

    public void CanWalk()
    {
        if (!_canMove) return;
        _currentSpeed = _defaultSpeed;
    }
    
    public void CantMove()
    {
        _canMove = false;
        _currentSpeed = 0;
    }

    public void CanMove()
    {
        _canMove = true;
        _currentSpeed = _defaultSpeed;
    }
}
