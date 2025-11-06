using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceViewPool : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer;
    [SerializeField] private ResourceView _prefab;
    [SerializeField] private Canvas _canvas;
    private int _batchSize = 10;
    private readonly Queue<ResourceView> _pool = new();
    readonly List<ResourceView> _active = new();

    private void Start()
    {
        SpawnBatch(_batchSize);
    }

    public void ActiveAddResourceView(Vector3 pos, Sprite sprite, int amount)
    {
        if (_pool.Count == 0) SpawnBatch(_batchSize);
        var item = _pool.Dequeue();
        _active.Add(item);
        item.Initialize(this, pos, sprite, amount);
    }

    public void Return(ResourceView item)
    {
        item.gameObject.SetActive(false);
        _active.Remove(item);
        _pool.Enqueue(item);
    }

    private void SpawnBatch(int n)
    {
        for (int i = 0; i < n; ++i)
        {
            var item = _diContainer.InstantiatePrefabForComponent<ResourceView>(_prefab, _canvas.transform);
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }
}
