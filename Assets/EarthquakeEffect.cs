using DG.Tweening;
using UnityEngine;

public class EarthquakeEffect : MonoBehaviour
{
    [SerializeField] private SetTileNeighbours _setTileNeighbours;
    [SerializeField] private TilesSystem _tilesSystem;
    [SerializeField] private Transform _transform;
    private float _initialYPosition = 10.8f;
    private float _shakeAmplitude = 0.2f;
    private float _shakeDuration = 2;
    private float _shakeSpeed = 0.07f;

    public void StartEarthquake()
    {
        _transform.position = new Vector3(_transform.position.x, _initialYPosition, _transform.position.z);
        
        Sequence shakeSequence = DOTween.Sequence();

        int shakeCount = Mathf.CeilToInt(_shakeDuration / (_shakeSpeed * 2));

        // Определяем момент для вызова метода на середине
        int halfwayPoint = shakeCount / 2;

        for (int i = 0; i < shakeCount; i++)
        {
            shakeSequence.Append(_transform.DOMoveY(_initialYPosition + _shakeAmplitude, _shakeSpeed).SetEase(Ease.InOutSine));
            shakeSequence.Append(_transform.DOMoveY(_initialYPosition - _shakeAmplitude, _shakeSpeed).SetEase(Ease.InOutSine));

            // Вставляем вызов метода в середине последовательности
            if (i == halfwayPoint)
            {
                shakeSequence.AppendCallback(OnShakeMidway);
            }
        }

        shakeSequence.Append(_transform.DOMoveY(_initialYPosition, _shakeSpeed).SetEase(Ease.OutSine));
        shakeSequence.Play();
    }

    private void OnShakeMidway()
    {
        for (int i = 0; i < _setTileNeighbours.TileObjects.Count; i++)
        {
            if(_setTileNeighbours.TileObjects[i].GroundTileObject().CheckTileView(GroundTileViewEnum.Mountain))
            {
                _setTileNeighbours.TileObjects[i].BuildingTileObject().DestroyBuildingTile(true);
                _setTileNeighbours.TileObjects[i].GroundTileObject().SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Volcano));
                _setTileNeighbours.TileObjects[i].GroundTileObject().SpawnGroundTile();
                return;
            }
        }
    }
}
