using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuantPickupPool : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer;
    [SerializeField] private QuantPickup _prefab;
    [SerializeField] private Canvas _canvas;
    private int _batchSize = 10;

    private readonly Queue<QuantPickup> _pool = new();
    readonly List<QuantPickup> _active = new();

    private void Start()
    {
        SpawnBatch(_batchSize);
    }

    public QuantPickupData[] GetActiveQuants()
    {
        var data = new QuantPickupData[_active.Count];

        for (int i = 0; i < _active.Count; i++)
        {
            Vector3 w = _active[i].GetWorldPos();
            data[i] = new QuantPickupData
            {
                PosX = w.x,
                PosY = w.y,
                PosZ = w.z,
                TimeLeft = _active[i].GetTimeLeft()
            };
        }
        return data;
    }

    public void LoadQuantPickup(QuantPickupData[] data)
    {
        if (data == null) return;
        foreach (var d in data)
        {
            ActiveQuantPickup(new Vector3(d.PosX, d.PosY, d.PosZ), d.TimeLeft);
        }
    }

    public QuantPickup ActiveQuantPickup(Vector3 pos, float remain = -1f)
    {
        if (_pool.Count == 0) SpawnBatch(_batchSize);
        var item = _pool.Dequeue();
        _active.Add(item);
        item.Initialize(this, pos, remain);
        return item;
    }

    public void Return(QuantPickup item)
    {
        item.gameObject.SetActive(false);
        _active.Remove(item);
        _pool.Enqueue(item);
    }

    private void SpawnBatch(int n)
    {
        for (int i = 0; i < n; ++i)
        {
            var item = _diContainer.InstantiatePrefabForComponent<QuantPickup>(_prefab, _canvas.transform);
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }
}
