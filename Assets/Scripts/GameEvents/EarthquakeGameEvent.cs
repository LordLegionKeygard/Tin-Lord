using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EarthquakeGameEvent : BaseGameEvent
{
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private Transform _transform;
    private float _initialYPosition = 10.8f;
    private float _shakeAmplitude = 0.2f;
    private float _shakeDuration = 2;
    private float _shakeSpeed = 0.07f;

    public override void StartEvent()
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
        var rnd = Random.Range(0, 2);
        var validTiles = new List<TileObject>();

        if (rnd == 0)
        {
            // Собираем все тайлы, которые соответствуют условию "Mountain"
            foreach (var tileObject in SetTileNeighbours().TileObjects)
            {
                if (tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Mountain))
                {
                    validTiles.Add(tileObject);
                }
            }

            // Если есть подходящие тайлы, выбираем случайный и выполняем действия
            if (validTiles.Count > 0)
            {
                var randomTile = validTiles[Random.Range(0, validTiles.Count)];
                randomTile.BuildingHealth().Death();
                randomTile.GroundTileObject().SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Volcano));
                randomTile.GroundTileObject().SpawnGroundTile();
            }
        }
        else
        {
            // Собираем все тайлы, которые НЕ являются "BaseFoundation"
            foreach (var tileObject in SetTileNeighbours().TileObjects)
            {
                if (tileObject.GroundTileObject().CurrentGroundTile() != null 
                && !tileObject.GroundTileObject().IsWaterTile()
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.BaseFoundation) 
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Crater)
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Road)
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Rift))
                {
                    validTiles.Add(tileObject);
                }
            }

            // Если есть подходящие тайлы, выбираем случайный и выполняем действия
            if (validTiles.Count > 0)
            {
                var randomTile = validTiles[Random.Range(0, validTiles.Count)];
                var previousGroundTileViewEnum = randomTile.GroundTileObject().CurrentGroundTile().GroundTileView;
                randomTile.BuildingHealth().Death();
                randomTile.GroundTileObject().SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Rift));
                randomTile.GroundTileObject().SpawnGroundTile(previousGroundTileViewEnum);
            }
        }
    }

}
