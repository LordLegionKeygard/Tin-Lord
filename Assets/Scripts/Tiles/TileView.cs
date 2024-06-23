using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    private BoxCollider _boxCollider;
    [SerializeField] private GameObject _selectView;
    [SerializeField] private Material _fourTileMaterial;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void SetBaseFoundationTile(Transform groundTransform, Transform groundParentTransform)
    {
        groundTransform.position = groundParentTransform.position + new Vector3(5, 0, 5);
        _boxCollider.center = new Vector3(5, -0.974f, 5);
        _boxCollider.size = new Vector3(19.5f, 1.95f, 19.5f);
        _selectView.transform.localScale = new Vector3(1.94f, 1.94f, 1.94f);
        _selectView.transform.position = _selectView.transform.position + new Vector3(5, 0, 5);
        _mesh.material =  _fourTileMaterial;
    }

    public void ViewToggle(bool state, SelectTileEnum selectTileEnum)
    {
        _selectView.SetActive(state);
        _mesh.material.SetColor("_BaseColor", Colors.Instance.SelectTileView[(int)selectTileEnum]);
    }

    public void TurnOffCollider() => _boxCollider.enabled = false;
}
