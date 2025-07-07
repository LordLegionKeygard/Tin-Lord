using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoMissionSystem : MonoBehaviour
{
    [SerializeField] private PanelDoMoveY _panelDoMoveY;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _avatar;
    [SerializeField] private Sprite[] _avatars;
    private Coroutine _coroutine;


    public void ShowInfo(string text, int avatarType, bool isWarning)
    {
        if (_panelDoMoveY.IsOpen()) return;
        if (isWarning) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.InfoWarning, transform.position);
        _infoText.text = text;
        _avatar.sprite = _avatars[avatarType];
        _panelDoMoveY.PanelMove(false);
        StartCoroutine(nameof(CloseCoroutine));
    }

    private IEnumerator CloseCoroutine()
    {
        yield return new WaitForSecondsRealtime(4f);

        _panelDoMoveY.PanelMove(false);
    }
}

[System.Serializable]
public enum AvatarType
{
    None = -1,
    Patch = 0,
}
