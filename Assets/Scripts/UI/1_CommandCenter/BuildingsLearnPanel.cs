using UnityEngine;
using UnityEngine.UI;

public class BuildingsLearnPanel : MonoBehaviour
{
    [SerializeField] private LearnBuildingItem[] _learnBuildingItems;
    [SerializeField] private ScrollRect _scrollRect;

    public LearnBuildingItem[] AllLearnBuildingItems() => _learnBuildingItems;

    public void ResetScrollPosition()
    {
        _scrollRect.verticalNormalizedPosition = 1f;
    }
}
