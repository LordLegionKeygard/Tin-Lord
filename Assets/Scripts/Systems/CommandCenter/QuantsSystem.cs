using TMPro;
using UnityEngine;

public class QuantsSystem : MonoBehaviour
{
    [SerializeField] private int _quants;
    [SerializeField] private TextMeshProUGUI _quantsText;

    public int GetQuants() => _quants;

    public void LoadQuants(int core)
    {
        _quants = core;
        UpdateView();
    }

    private void UpdateView()
    {
        _quantsText.text = _quants.ToString();
    }

    public void ChangeQuants(int value)
    {
        _quants += value;
        UpdateView();
    }
}
