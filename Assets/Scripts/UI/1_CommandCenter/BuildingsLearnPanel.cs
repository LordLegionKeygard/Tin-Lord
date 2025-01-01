using TMPro;
using UnityEngine;

public class BuildingsLearnPanel : MonoBehaviour
{
    private float _memoryFragments;
    [SerializeField] private TextMeshProUGUI _memoryFragmentsText;
    [SerializeField] private LearnBuildingItem[] _learnBuildingItems;

    public LearnBuildingItem[] AllLearnBuildingItems() => _learnBuildingItems;
    public void SetFragments(float fragments) => _memoryFragments = fragments;
    public float MemoryFragments() => _memoryFragments;
    public bool IsFragmentEnought(float fragments) => fragments <= _memoryFragments;

    private void Start()
    {
        CustomEvents.OnDataLoad += SetupData;
    }

    public void SetupData()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _memoryFragmentsText.text = $"{Language.TextStatic[43]} {_memoryFragments}";
    }

    public void ChangeFragments(float fragments)
    {
        _memoryFragments -= fragments;
        UpdateText();
    }

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetupData;
    }
}
