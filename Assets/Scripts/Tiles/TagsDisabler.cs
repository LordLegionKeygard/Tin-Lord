using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsDisabler : MonoBehaviour
{
    [SerializeField] private Tags[] _tags;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Tag>(out Tag tag))
        {
            tag.CheckTag(_tags);
        }
    }
}
