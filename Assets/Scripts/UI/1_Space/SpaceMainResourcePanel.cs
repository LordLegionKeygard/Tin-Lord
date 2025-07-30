using UnityEngine;
using UnityEngine.UI;

public class SpaceMainResourcePanel : MonoBehaviour
{
    [SerializeField] private PanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;

    public void PanelMove(bool needSound = true)
    {
        if (_button.enabled == false) return;
        _panelDoMoveX.PanelMove(needSound);
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.SpaceOpenResourcePanel_3);
    }
}
