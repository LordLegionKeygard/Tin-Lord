using UnityEngine;

public class EnemyCenterPoint : MonoBehaviour
{
    [SerializeField] private Transform _centerPoint;
    public Transform GetTransform() => _centerPoint;
}
