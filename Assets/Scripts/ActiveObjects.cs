using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private bool _isRotation;
    [SerializeField] private int _min;
    [SerializeField] private int _max;
    private int _currentObjectsCount;

    private void Start()
    {
        Active();
    }

    private void Active()
    {
        var objectsCount = Random.Range(_min, _max);

        for (int i = 0; i < _objects.Length; i++)
        {
            if (_currentObjectsCount >= objectsCount) return;

            var rnd = Random.Range(0, 100);

            if (rnd > 50 && !_objects[i].activeInHierarchy)
            {
                _objects[i].SetActive(true);
                if(_isRotation) RandomRotation(i);
                _currentObjectsCount++;
            }
        }

        if(_currentObjectsCount < objectsCount) Active();
    }

    private void RandomRotation(int number)
    {
        var rnd = Random.Range(0, 360);

        _objects[number].transform.rotation = Quaternion.Euler(0, rnd * 2, 0);
    }
}
