using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcesItemView : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;
    [SerializeField] private Resource _resource;

    private void Start()
    {
        // _name.text = _resource.Name[Language.LanguageNumber];
        _image.sprite = _resource.Icon;
    }
}
