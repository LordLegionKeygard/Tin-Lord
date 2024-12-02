using Pathfinding;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    [SerializeField] private bool _canMove = true;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _speedMultiplier = 1.0f; // Текущий множитель скорости (1.0 = 100%)
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }

    public void ChangeSlow(float slowAmount)
    {
        _speedMultiplier = Mathf.Clamp(_speedMultiplier + slowAmount, 0.1f, 1.0f); // Минимум 10% от базовой скорости
        CanRun();
    }

    public void CanRun()
    {
        if (!_canMove) return;
        _aiPath.maxSpeed = _defaultSpeed * _speedMultiplier;
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
