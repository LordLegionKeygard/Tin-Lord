using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Animator _animator;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private GameObject _selectView;
    [SerializeField] private Material _fourTileMaterial;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
    }

    public void SetTileView(Transform groundTransform, Tile tile)
    {
        switch (tile.GroundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                groundTransform.position += new Vector3(5, 0, 5);
                _boxCollider.center = new Vector3(5, -0.974f, 5);
                _boxCollider.size = new Vector3(19.5f, 1.95f, 19.5f);
                _selectView.transform.localScale = new Vector3(1.94f, 1.94f, 1.94f);
                _selectView.transform.position += new Vector3(5, 0, 5);
                _mesh.material = _fourTileMaterial;
                break;
            case GroundTileViewEnum.Mountain or GroundTileViewEnum.OilSwamp:
                _selectView.transform.localPosition = new Vector3(-0.35f, 0.5f, -0.35f);
                break;
        }
    }

    public void PlayAnimation(int animation)
    {
        _animator.SetTrigger(animation);
    }


    public void ViewToggle(bool state, SelectTileEnum selectTileEnum)
    {
        _selectView.SetActive(state);
        _mesh.material.SetColor("_BaseColor", Colors.Instance.SelectTileView[(int)selectTileEnum]);
    }

    public void TurnOffCollider() => _boxCollider.enabled = false;
}
