using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _tooltip;

    private void Start()
    {
        CustomEvents.OnTooltipToggle += ShowToggle;
        CustomEvents.OnCloseTooltips += CloseAll;
    }

    public void ShowToggle(bool state, int toolTipNumer)
    {
        _tooltip[toolTipNumer].SetActive(state);
    }

    public void CloseAll()
    {
        foreach (var item in _tooltip)
        {
            item.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnTooltipToggle -= ShowToggle;
        CustomEvents.OnCloseTooltips -= CloseAll;
    }
}
