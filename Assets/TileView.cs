using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private GameObject _selectView;

    public void ViewToggle(bool state) => _selectView.SetActive(state);
}
