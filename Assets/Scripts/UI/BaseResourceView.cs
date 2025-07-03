using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseResourceView : MonoBehaviour
{
    [SerializeField] protected ResourceSpritesInfo _resourceSpritesInfo;
    [SerializeField] protected GameObject[] _resourceCells;
    [SerializeField] protected Image[] _icons;
    [SerializeField] protected TextMeshProUGUI[] _amountText;

    public virtual void SetResourcesView(ResourceWrapper[] resources)
    {
        ResetCells();
    }


    public virtual void ResetCells()
    {
        foreach (var item in _resourceCells)
        {
            item.SetActive(false);
        }
    }
}
