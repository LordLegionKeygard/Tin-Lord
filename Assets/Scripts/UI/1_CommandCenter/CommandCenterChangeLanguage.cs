using TMPro;
using UnityEngine;

public class CommandCenterChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;

    private void Start()
    {
        _headerText.text = Language.TextStatic[32];
    }
}
