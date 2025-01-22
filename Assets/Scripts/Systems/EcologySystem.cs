using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EcologySystem : MonoBehaviour
{
    [Header("Ecology")]
    [SerializeField] private int _tilesEcology;
    [SerializeField] private int _radiation;
    [SerializeField] private int _missionEcology;
    [SerializeField] private int _totalEcology;

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
    private Coroutine _changeTextCoroutine;
    private float _currentRotationAngle = 0f;

    public int GetRadiation() => _radiation;


    private void Start()
    {
        CustomEvents.OnChangeEcology += ChangeEcology;
        CustomEvents.OnDataLoad += UpdateTotalEcology;
    }

    public void LoadEcology(int radiation)
    {
        _missionEcology = CurrentMissionInfo.Instance.GetCurrentMission().StartEcology;
        _radiation = radiation;
        UpdateRadiationView();
        UpdateTotalEcology();
    }

    public void ChangeEcology(int amount, int tileId, bool remove)
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
        _radiationText.text = $"{Language.TextStatic[5]} {_radiation:D2}";
        _radiationIcon.sprite = _radiation < 25 ? _radiationSprites[0] : _radiationSprites[1];
    }

    private void UpdateTotalEcology()
    {
        int previousTotalEcology = _totalEcology;
        _tilesEcology = _ecologyTileInfoList.Sum(tile => tile.Amount);

        _totalEcology = _tilesEcology - _radiation + _missionEcology;

        CheckLimitEcology();

        if (_changeTextCoroutine != null)
        {
            StopCoroutine(_changeTextCoroutine);
        }

        _changeTextCoroutine = StartCoroutine(ChangeTextSmoothly(previousTotalEcology, _totalEcology));
        UpdateGearRotation(previousTotalEcology, _totalEcology);  // Передаем старое и новое значение экологии

        _setupRenderSettings.UpdateEcologyRender(_totalEcology);
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.RestoreEcology, _totalEcology);
    }

    private IEnumerator ChangeTextSmoothly(int oldValue, int newValue)
    {
        float elapsed = 0f;

        while (elapsed < _changeTextDuration)
        {
            elapsed += Time.deltaTime;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(oldValue, newValue, elapsed / _changeTextDuration));
            UpdateEcologyText(currentValue);
            yield return null;
        }
        UpdateEcologyText(newValue);
    }

    private void UpdateGearRotation(int previousEcology, int newEcology)
    {
        int changeAmount = newEcology - previousEcology;

        if (changeAmount == 0) return;

        _warningSign.SetActive(false);

        float rotationAngle = changeAmount > 0 ? 45f : -45f;

        _gearRectTransform.DOKill();

        _currentRotationAngle += rotationAngle;

        _gearRectTransform.DORotate(new Vector3(0, 0, _currentRotationAngle), _changeTextDuration)
            .OnComplete(() => UpdateWarningSign(newEcology));
    }



    private void UpdateWarningSign(int ecologyValue)
    {
        _warningSign.SetActive(ecologyValue <= -50);
    }

    private void UpdateEcologyText(int ecologyValue)
    {
        _totalEcologyText.color = ecologyValue < 0 ? Colors.WarningYellow : ecologyValue > 50 ? Colors.LightGreen : Colors.GreySeven;

        var ecologyString = Mathf.Abs(ecologyValue).ToString("D2");
        _totalEcologyText.text = ecologyString;
    }

    private void CheckLimitEcology()
    {
        if (_totalEcology < -99) _totalEcology = -99;
        else if (_totalEcology > 99) _totalEcology = 99;
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeEcology -= ChangeEcology;
        CustomEvents.OnDataLoad -= UpdateTotalEcology;
    }
}

[System.Serializable]
public class EcologyTileInfo
{
    public int Id;
    public int Amount;
}
