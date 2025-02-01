using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EarthquakeWorldEvent : BaseWorldEvent
{
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private Transform _transform;
    private float _delay = 1.5f;
    private float _initialYPosition = 10.8f;
    private float _shakeAmplitude = 0.2f;
    private float _shakeDuration = 2;
    private float _shakeSpeed = 0.07f;

    public override void StartEvent()
    {
        StartCoroutine(nameof(EarthquakeCoroutine));
    }

    private IEnumerator EarthquakeCoroutine()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EarthQuake, transform.position);
        yield return new WaitForSeconds(_delay);

        _transform.position = new Vector3(_transform.position.x, _initialYPosition, _transform.position.z);

        Sequence shakeSequence = DOTween.Sequence();

        int shakeCount = Mathf.CeilToInt(_shakeDuration / (_shakeSpeed * 2));

        for (int i = 0; i < shakeCount; i++)
        {
            shakeSequence.Append(_transform.DOMoveY(_initialYPosition + _shakeAmplitude, _shakeSpeed).SetEase(Ease.InOutSine));
            shakeSequence.Append(_transform.DOMoveY(_initialYPosition - _shakeAmplitude, _shakeSpeed).SetEase(Ease.InOutSine));

            if (i == shakeCount - 1)
            {
                shakeSequence.AppendCallback(UseEarthQuake);
            }
        }

        shakeSequence.Append(_transform.DOMoveY(_initialYPosition, _shakeSpeed).SetEase(Ease.OutSine));
        shakeSequence.Play();
    }

    private void UseEarthQuake()
    {
        var rnd = Random.Range(0, 2);
        var validTiles = new List<TileObject>();

        if (rnd == 0)
        {
            // Собираем все тайлы, которые соответствуют условию "Mountain"
            foreach (var tileObject in GetAllTileObjects().TileObjects)
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
                randomTile.GroundTileObject().SetGroundTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Volcano));
                randomTile.GroundTileObject().SpawnGroundTile();
            }
        }
        else
        {
            // Собираем все тайлы, которые НЕ являются "BaseFoundation"
            foreach (var tileObject in GetAllTileObjects().TileObjects)
            {
                if (tileObject.GroundTileObject().CurrentGroundTile() != null
                && !tileObject.GroundTileObject().IsWaterTile()
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.BaseFoundation)
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Crater)
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Road)
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Rift)
                && !tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Volcano))
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
                randomTile.GroundTileObject().SetGroundTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Rift));
                randomTile.GroundTileObject().SpawnGroundTile(previousGroundTileViewEnum);
            }
        }
    }

}
