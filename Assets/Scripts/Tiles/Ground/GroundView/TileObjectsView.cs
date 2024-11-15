using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectsView : MonoBehaviour
{
    [SerializeField] private ActiveObjects[] _activeObjects;

    private void Start()
    {
        RefreshObjects();
    }

    public void RefreshObjects()
    {
        for (int i = 0; i < _activeObjects.Length; i++)
        {
            _activeObjects[i].Refresh();
        }
    }
}
