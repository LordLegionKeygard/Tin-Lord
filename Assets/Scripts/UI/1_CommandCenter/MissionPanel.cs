using TMPro;
using UnityEngine;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private MissionItem[] _missionItems;
    [SerializeField] private TextMeshProUGUI _missionNameText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _ecologyLevelText;
    [SerializeField] private TextMeshProUGUI _startResourcesText;
    [SerializeField] private TextMeshProUGUI _objectiveText;


    public void RefreshInfo(Mission mission)
    {
        UnselectAllMission();
    }

    private void UnselectAllMission()
    {
        foreach (var item in _missionItems)
        {
            item.SelectToggleView(false);
        }
    }
}
