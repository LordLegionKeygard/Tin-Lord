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


    private void Awake()
    {
        CustomEvents.OnChangeEcology += ChangeEcology;
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
        _totalEcology = _ecologyTileInfoList.Sum(tile => tile.Amount);

        _totalEcologyText.color = _totalEcology < 0 ? Colors.WarningSign : Colors.TextGrey;
        _warningSign.SetActive(_totalEcology <= -50);

        var ecologyString = Mathf.Abs(_totalEcology).ToString("D2");

        _totalEcologyText.text = ecologyString;
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
