using UnityEngine;

public class BasePoint : MonoBehaviour
{
    public static BasePoint Instance;
    [SerializeField] private Transform[] _basePoints;
    public Transform[] GetBasePoints() => _basePoints;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetRandomBasePoint()
    {
        var rnd = Random.Range(0, _basePoints.Length);
        return _basePoints[rnd];
    }
}
