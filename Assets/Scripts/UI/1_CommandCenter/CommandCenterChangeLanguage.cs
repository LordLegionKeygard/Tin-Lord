using TMPro;
using UnityEngine;

public class CommandCenterChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _missionNameText;

    private void Start()
    {
        _missionNameText.text = Language.TextStatic[32];
    }
}
