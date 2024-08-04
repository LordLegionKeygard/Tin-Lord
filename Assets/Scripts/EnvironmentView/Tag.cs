using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private Tags _currentTag;
    public void CheckTag(Tags[] tags)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (_currentTag == tags[i] || tags[0] == Tags.All)
            {
                _parentObject.SetActive(false);
            }
        }
    }
}

public enum Tags
{
    All = 0,
    Tree = 1,
    Coal = 2,
    Plant = 3,
}