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
    [SerializeField] private TerminalActTexts[] _terminalActTexts;

    public void ActiveTerminal(int act)
    {
        CustomEvents.FireControlFadeMusic(false, MusicType.Main);
        UnactiveObjects();
        ActiveRender();
        _terminal.SetActive(true);
        _consoleTextsTyping.StartTyping(_terminalActTexts[act].ConsoleTextsIndexes);
        _storyTextTyping.StartTyping(_terminalActTexts[act].StoryTextsIndexes);
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

[System.Serializable]
public class TerminalActTexts
{
    public int[] StoryTextsIndexes;
    public int[] ConsoleTextsIndexes;
}
