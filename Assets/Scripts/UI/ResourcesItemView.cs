using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Resource _resource;

    private void Start()
    {
        _name.text = _resource.Name[Language.LanguageNumber];
    }
}
