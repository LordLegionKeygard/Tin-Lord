using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionHolderPanel : MonoBehaviour
{
    [Inject] TutorialSystem _tutorialSystem;
    [SerializeField] private DownPanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;


    public void PanelMove()
    {
        if (!_tutorialSystem.IsCompleteMissionTutorial())
        {
            if (_tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionSkillsPanel) return;
        }
        
        if (_button.enabled == false) return;

        _panelDoMoveX.PanelMove();
    }
}
