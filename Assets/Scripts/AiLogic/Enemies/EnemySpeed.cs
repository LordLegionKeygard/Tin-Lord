using Pathfinding;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    [SerializeField] private bool _canMove = true;
    [SerializeField] private float _defaultSpeed;
    private AIPath _aiPath;
    private EnemyDebuff _enemyDebuff;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _enemyDebuff = GetComponent<EnemyDebuff>();
    }

    public void CanRun()
    {
        if (!_canMove) return;
        _aiPath.maxSpeed = _defaultSpeed * _enemyDebuff.GetSpeedFactor();
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
