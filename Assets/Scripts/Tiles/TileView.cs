using UnityEngine;
using DG.Tweening;

public class TileView : MonoBehaviour
{
    private BoxCollider _boxCollider;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private GameObject _selectView;
    [SerializeField] private Material _fourTileMaterial;
    [SerializeField] private GameObject[] _edgeViews;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void SetTileView(Transform groundTransform, Tile tile)
    {
        switch (tile.GroundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                groundTransform.position += new Vector3(5, 0, 5);
                _boxCollider.center = new Vector3(5, -0.974f, 5);
                _boxCollider.size = new Vector3(19.5f, 1.95f, 19.5f);
                _selectView.transform.localScale = new Vector3(1.97f, 1.97f, 1.97f);
                _selectView.transform.position += new Vector3(5, 0, 5);
                _mesh.material = _fourTileMaterial;
                break;
            case GroundTileViewEnum.Mountain or GroundTileViewEnum.OilSwamp or GroundTileViewEnum.Volcano or GroundTileViewEnum.OvergrownMountain:
                _selectView.transform.localPosition = new Vector3(-0.35f, 0.5f, -0.35f);
                break;
        }
    }

    public void PlayAnimation(TileAnimationsEnum tileAnimationsEnum, TweenCallback onComplete = null)
    {
        // Снимаем все активные твины с этого transform, чтобы не было накопления
        transform.DOKill();

        switch (tileAnimationsEnum)
        {
            case TileAnimationsEnum.Spawn:
                transform.localScale = Vector3.one * 0.6f;
                transform.DOScale(1f, 0.2f).SetEase(Ease.InOutSine).SetUpdate(true)
                    .OnComplete(onComplete);
                break;
            case TileAnimationsEnum.Destroy:
                transform.DOScale(0, 2f).SetEase(Ease.InOutSine).SetUpdate(true)
                    .OnComplete(onComplete);
                break;
        }
    }

    public void SelectViewToggle(bool state, SelectTileEnum selectTileEnum)
    {
        _selectView.SetActive(state);
        _mesh.material.SetColor("_BaseColor", Colors.Instance.SelectTileView[(int)selectTileEnum]);
    }

    public void EdgeViewToggle(float x, float y, bool state)
    {
        _edgeViews[0].SetActive(y == 0 && state);
        _edgeViews[1].SetActive(y == CurrentMissionInfo.Instance.GetCurrentLandscape().MapWidth * 10 - 10 && state);
        _edgeViews[2].SetActive(x == CurrentMissionInfo.Instance.GetCurrentLandscape().MapLength * 10 - 10 && state);
        _edgeViews[3].SetActive(x == 0 && state);
    }

    public void TurnOffCollider() => _boxCollider.enabled = false;
}

[System.Serializable]
public enum TileAnimationsEnum
{
    Spawn = 0,
    Destroy = 1,
}
