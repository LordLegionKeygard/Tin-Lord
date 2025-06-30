using TMPro;
using UnityEngine;

public class ShardsSystem : MonoBehaviour
{
    [SerializeField] private int _shards;
    [SerializeField] private TextMeshProUGUI _shardsText;

    public int GetShards() => _shards;

    public void LoadShards(int value)
    {
        _shards = value;
        UpdateView();
    }

    private void UpdateView()
    {
        _shardsText.text = $"{Language.TextStatic[83]}: {_shards}";
    }

    public void ChangeShards(int value)
    {
        _shards += value;
        UpdateView();
    }
}
