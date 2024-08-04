using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EcologySystem : MonoBehaviour
{
    [SerializeField] private int _totalEcology;
    [SerializeField] private TextMeshProUGUI _totalEcologyText;
    [SerializeField] private List<EcologyTileInfo> _ecologyTileInfoList = new List<EcologyTileInfo>();


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
        var totalEcology = 0;
        for (int i = 0; i < _ecologyTileInfoList.Count; i++)
        {
            totalEcology += _ecologyTileInfoList[i].Amount;
        }

        _totalEcology = totalEcology;
        _totalEcologyText.text = _totalEcology.ToString();
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
