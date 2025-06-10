using System.Collections;
using FMODUnity;
using UnityEngine;

public class TerminalSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _falseObjects;
    [SerializeField] private StudioEventEmitter _eventEmitter;
    [SerializeField] private SetupRenderSettings _setupRenderSettings;
    [SerializeField] private GameObject _terminal;
    [SerializeField] private ConsoleTextsTyping _consoleTextsTyping;
    [SerializeField] private StoryTextTyping _storyTextTyping;
    [SerializeField] private EndMissionSystem _endMissionSystem;

    public void ActiveTerminal()
    {
        CustomEvents.FireControlFadeMusic(false, MusicType.Main);
        UnactiveObjects();
        ActiveRender();
        _terminal.SetActive(true);
        _consoleTextsTyping.StartTyping(CurrentMissionInfo.Instance.GetCurrentLandscape().ConsoleTextsIndexes);
        _storyTextTyping.StartTyping(CurrentMissionInfo.Instance.GetCurrentLandscape().StoryTextsIndexes);
        _eventEmitter.Play();
    }

    private void UnactiveObjects()
    {
        foreach (var item in _falseObjects)
        {
            item.SetActive(false);
        }
    }

    private void ActiveRender()
    {
        _setupRenderSettings.SetTerminalRender();
    }

    public void ContinueButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Terminal], transform.position);
        CustomEvents.FireControlFadeMusic(false, MusicType.Terminal);
        _endMissionSystem.LoadCommandCenter();
    }
}
