using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EcologySystem : MonoBehaviour
{
    [SerializeField] private int _totalEcology;
    [SerializeField] private TextMeshProUGUI _totalEcologyText;
    [SerializeField] private List<EcologyTileInfo> _ecologyTileInfoList = new List<EcologyTileInfo>();
    [SerializeField] private GameObject _warningSign;
    private float _changeTextDuration = 1;
    private SetupRenderSettings _setupRenderSettings;
    private Coroutine _changeTextCoroutine;

    private void Awake()
    {
        CustomEvents.OnChangeEcology += ChangeEcology;
        _setupRenderSettings = GetComponent<SetupRenderSettings>();
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

    private void UpdateTotalEcology()
    {
        int previousEcology = _totalEcology;
        _totalEcology = _ecologyTileInfoList.Sum(tile => tile.Amount);

        CheckLimitEcology();

        if (_changeTextCoroutine != null)
        {
            StopCoroutine(_changeTextCoroutine);
        }
        
        _changeTextCoroutine = StartCoroutine(ChangeTextSmoothly(previousEcology, _totalEcology));

        _setupRenderSettings.UpdateRenderSettings(_totalEcology);
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

    private void UpdateEcologyText(int ecologyValue)
    {
        _totalEcologyText.color = ecologyValue < 0 ? Colors.WarningYellow : Colors.Grey;
        _warningSign.SetActive(ecologyValue <= -50);

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
    }
}

[System.Serializable]
public class EcologyTileInfo
{
    public int Id;
    public int Amount;
}
