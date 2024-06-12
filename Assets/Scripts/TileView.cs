using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private GameObject _selectView;
    [SerializeField] private Material _tileSelectViewMaterial;

    public void ViewToggle(bool state, TileTypeEnum tileTypeEnum)
    {
        _selectView.SetActive(state);
        _tileSelectViewMaterial.SetColor("_BaseColor", Colors.Instance.SelectTileView[(int)tileTypeEnum]);

    }
}
