using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EcologySystem : MonoBehaviour
{
    [Inject] private EnemyDefenceSystem _enemyDefenceSystem;

    [Header("Ecology")]
    [SerializeField] private List<int> _everyDayEcology;
    [SerializeField] private float _tilesEcology;
    [SerializeField] private int _radiation;
    [SerializeField] private int _missionEcology;
    [SerializeField] private float _totalEcology;

    [Header("View")]
    [SerializeField] private RectTransform _gearRectTransform;
    [SerializeField] private TextMeshProUGUI _totalEcologyText;
    [SerializeField] private TextMeshProUGUI _radiationText;
    [SerializeField] private GameObject _warningSign;
    [SerializeField] private Image _radiationIcon;
    [SerializeField] private Sprite[] _radiationSprites;

    [Header("Other")]
    [SerializeField] private List<EcologyTileInfo> _ecologyTileInfoList = new List<EcologyTileInfo>();
    [SerializeField] private SetupRenderSettings _setupRenderSettings;
    private readonly float _changeTextDuration = 1;
    private Tween _changeTextTween;
    private float _currentRotationAngle = 0f;

    public int GetRadiation() => _radiation;
    public float GetTotalEcology() => _totalEcology;
    public int[] GetEveryDayEcology() => _everyDayEcology.ToArray();


    private void Start()
    {
        CustomEvents.OnChangeEcology += ChangeEcology;
        CustomEvents.OnDataLoad += UpdateTotalEcology;
        CustomEvents.OnDayEnd += SetDayEcology;
    }

    private void SetDayEcology(int _)
    {
        _everyDayEcology.Add((int)_totalEcology);
    }

    public void LoadEcology(int radiation, int[] everyDayEcology, bool isStartMission)
    {
        if (!isStartMission)
        {
            _everyDayEcology.Clear();
            _everyDayEcology = everyDayEcology.ToList();
        }

        _missionEcology = CurrentMissionInfo.Instance.GetCurrentLandscape().StartEcology;
        _radiation = radiation;
        UpdateRadiationView();
        UpdateTotalEcology();
    }

    public void ChangeEcology(float amount, int tileId, bool remove)
    {
        for (int i = 0; i < _ecologyTileInfoList.Count; i++)
        {
            if (_ecologyTileInfoList[i].Id == tileId)
            {
                if (remove)
                {
                    _ecologyTileInfoList.Remove(_ecologyTileInfoList[i]);
                }
                else
                {
                    _ecologyTileInfoList[i].Amount = amount;
                }
                UpdateTotalEcology();
                return;
            }
        }

        _ecologyTileInfoList.Add(new EcologyTileInfo
        {
            Id = tileId,
            Amount = amount,
        });
        UpdateTotalEcology();
    }

    public void ChangeRadiation(int amount)
    {
        _radiation += amount;
        UpdateRadiationView();
        UpdateTotalEcology();
    }

    private void UpdateRadiationView()
    {
        if (_radiation < 0) _radiation = 0;
        if (_radiation > WorldGameInfo.MaximumRadiation) _radiation = WorldGameInfo.MaximumRadiation;
        _radiationText.text = $"{Language.TextStatic[5]}: {_radiation:D2}";
        _radiationIcon.sprite = _radiation < 25 ? _radiationSprites[0] : _radiationSprites[1];
    }

    private void UpdateTotalEcology()
    {
        float previousTotalEcology = _totalEcology;
        _tilesEcology = _ecologyTileInfoList.Sum(tile => (int)tile.Amount);

        _totalEcology = _tilesEcology - _radiation + _missionEcology;

        CheckLimitEcology();


        AnimateEcologyText((int)previousTotalEcology, (int)_totalEcology);
        UpdateGearRotation(previousTotalEcology, _totalEcology);

        _setupRenderSettings.UpdateEcologyRender((int)_totalEcology);
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.RestoreEcology, (int)_totalEcology);
        _enemyDefenceSystem.ChangeDefence();
    }

    private void AnimateEcologyText(int oldValue, int newValue)
    {
        _changeTextTween?.Kill();

        _changeTextTween = DOTween.To(() => oldValue, val => UpdateEcologyText(val), newValue, _changeTextDuration).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void UpdateGearRotation(float previousEcology, float newEcology)
    {
        float changeAmount = newEcology - previousEcology;

        if (changeAmount == 0) return;

        _warningSign.SetActive(false);

        float rotationAngle = changeAmount > 0 ? 45f : -45f;

        _gearRectTransform.DOKill();

        _currentRotationAngle += rotationAngle;

        _gearRectTransform.DORotate(new Vector3(0, 0, _currentRotationAngle), _changeTextDuration).SetUpdate(true)
        .OnComplete(() => UpdateWarningSign((int)newEcology));
    }



    private void UpdateWarningSign(int ecologyValue)
    {
        _warningSign.SetActive(ecologyValue <= -50);
    }

    private void UpdateEcologyText(int ecologyValue)
    {
        switch (ecologyValue)
        {
            case <= -50:
                _totalEcologyText.color = Colors.WarningRed;
                break;
            case < 0:
                _totalEcologyText.color = Colors.WarningYellow;
                break;
            case >= 50:
                _totalEcologyText.color = Colors.Green;
                break;
            case >= 25:
                _totalEcologyText.color = Colors.LightGreen;
                break;
            default:
                _totalEcologyText.color = Colors.GreySeven;
                break;
        }

        var ecologyString = Mathf.Abs(ecologyValue).ToString("D2");
        _totalEcologyText.text = ecologyString;
    }

    private void CheckLimitEcology()
    {
        if (_totalEcology < -99) _totalEcology = -99;
        else if (_totalEcology > 99) _totalEcology = 99;
    }

    public float GetEcologyBonus()
    {
        var everyDayEcology = GetEveryDayEcology();
        int totalMiddleEcology = 0;

        if (everyDayEcology.Length > 0)
        {
            int sum = 0;
            for (int i = 0; i < everyDayEcology.Length; i++)
            {
                sum += everyDayEcology[i];
            }
            totalMiddleEcology = sum / everyDayEcology.Length;
        }

        if (totalMiddleEcology <= -75) return 0.25f;
        if (totalMiddleEcology <= -50) return 0.5f;
        if (totalMiddleEcology <= -25) return 0.75f;
        if (totalMiddleEcology <= 0) return 1f;
        if (totalMiddleEcology <= 25) return 1.25f;
        if (totalMiddleEcology <= 50) return 1.5f;
        if (totalMiddleEcology <= 75) return 1.75f;

        return 2f;
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeEcology -= ChangeEcology;
        CustomEvents.OnDataLoad -= UpdateTotalEcology;
        CustomEvents.OnDayEnd -= SetDayEcology;
    }
}

[System.Serializable]
public class EcologyTileInfo
{
    public int Id;
    public float Amount;
}
