using UnityEngine;

public class TagsDisabler : MonoBehaviour
{
    [SerializeField] private Tags[] _tags;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tag>(out Tag tag))
        {
            tag.CheckTag(_tags);
        }
    }
}
