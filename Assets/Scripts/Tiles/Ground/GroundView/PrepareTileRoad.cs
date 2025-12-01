using UnityEngine;

public class PrepareTileRoad : MonoBehaviour
{
    [SerializeField] private GameObject[] _roads;

    public void SetRoad(int number, int rotation)
    {
        _roads[number].SetActive(true);
        _roads[number].transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
