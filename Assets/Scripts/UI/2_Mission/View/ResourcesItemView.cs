using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcesItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Resource _resource;

    private void Start()
    {
        _image.sprite = _resource.Icon;
    }
}
