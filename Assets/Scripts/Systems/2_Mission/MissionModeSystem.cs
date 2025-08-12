using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionModeSystem : MonoBehaviour
{
    private bool _isPlanetMode = true;
    public bool IsPlanetMode() => _isPlanetMode;
    [SerializeField] private UIPanelsMission _uiPanelsMission;

    [Header("View")]
    [SerializeField] private Image _modeImage;
    [SerializeField] private Sprite[] _modeSprites;
    [SerializeField] private TextMeshProUGUI _modeText;

    public void LoadMode(bool isPlanetMode)
    {
        _isPlanetMode = isPlanetMode;
        ChangeView();
    }

    public void ChangeMode()
    {
        _isPlanetMode = !_isPlanetMode;
        ChangeView();
    }

    private void ChangeView()
    {
        _modeImage.sprite = _modeSprites[_isPlanetMode ? 0 : 1];
        _modeText.text = Language.TextStatic[_isPlanetMode ? 226 : 227];
        _modeText.color = _isPlanetMode ? Colors.GreySeven : Colors.WarningRed;

        if (_isPlanetMode)
        {

        }
        else
        {
            _uiPanelsMission.PreparePanelsToShipMode();
        }
    }
}
