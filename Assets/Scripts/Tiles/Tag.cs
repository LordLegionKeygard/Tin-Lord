using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private Tags _currentTag;
    private bool _canCheckTags = true;

    private void Start()
    {
        CustomEvents.OnToggleCheckTags += ChangeCheckTags;
    }

    private void ChangeCheckTags(bool state) => _canCheckTags = state;

    public void CheckTag(Tags[] tags)
    {
        if (!_canCheckTags) return;

        for (int i = 0; i < tags.Length; i++)
        {
            if (_currentTag == tags[i] || tags[0] == Tags.All)
            {
                _parentObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnToggleCheckTags -= ChangeCheckTags;
    }
}

public enum Tags
{
    All = 0,
    Tree = 1,
    Coal = 2,
    Plant = 3,
    Rock = 4,
}