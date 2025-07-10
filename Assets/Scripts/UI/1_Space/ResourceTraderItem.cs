using UnityEngine;
using UnityEngine.UI;

public class ResourceTraderItem : MonoBehaviour
{
    [SerializeField] private ResourceTraderPanel _resourceTraderPanel;
    [SerializeField] private Resource _resource;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _iconView;
    [SerializeField] private GameObject _selectView;
    [SerializeField] private GameObject _closeTextView;

    public void Select()
    {
        _resourceTraderPanel.SelectResource(_resource);
    }

    public void SelectToggle(bool state)
    {
        _selectView.SetActive(state);
    }

    public void SetResourceOpen(int currentAct)
    {
        var open = (int)_resource.ResourceType <= currentAct;
        _button.interactable = open;
        _closeTextView.SetActive(!open);
        _iconView.SetActive(open);
    }
}
