using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UINode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Inject] private readonly TutorialSystem _tutorialSystem;

    [Header("Graphics")]
    [SerializeField] private GameObject _tutorialArrow;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _availableView;
    private NodeData _nodeData;
    private int _index;
    private MapSystem _mapSystem;
    private Color _defaultColor;
    private bool _available;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += ActiveTutorialArrow;
        CustomEvents.OnCompleteTutorialStep += DisableTutorialArrow;
    }

    private void OnDestroy()
    {
        CustomEvents.OnStartTutorialStep -= ActiveTutorialArrow;
        CustomEvents.OnCompleteTutorialStep -= DisableTutorialArrow;
    }

    public void DisableTutorialArrow(TutorialStepEnum tutorialStepEnum)
    {
        if (tutorialStepEnum != TutorialStepEnum.SpaceSelectNode_7) return;

        _tutorialArrow.SetActive(false);
    }

    public void ActiveTutorialArrow(TutorialStepEnum tutorialStepEnum)
    {
        if (tutorialStepEnum != TutorialStepEnum.SpaceSelectNode_7 || !_available) return;

        _tutorialArrow.SetActive(true);
    }


    public int GetDescriptionTextNumber()
    {
        if (_mapSystem.IsCurrent(_index))
            return 292;

        if (_mapSystem.IsVisited(_index))
            return 293;

        return _nodeData.DescriptionTextNumber;
    }

    public void Setup(NodeData data, int index, MapSystem map)
    {
        _nodeData = data;
        _index = index;
        _mapSystem = map;

        _defaultColor = _nodeData.IconColor;
        _icon.sprite = _nodeData.Icon;
        _icon.color = _defaultColor;
        _icon.rectTransform.sizeDelta = new Vector2(_nodeData.IconWidth, _nodeData.IconHeight);

        SetAvailable(false);
        SetCompleted(false);
    }

    public void SelectNodeButton()
    {
        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.SpaceMapDescription_6) return;
        if (!_tutorialSystem.IsCompleteAllTutorial()) CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.SpaceSelectNode_7);

        _mapSystem.TrySelectNode(_index);
    }

    public void SetAvailable(bool state)
    {
        _available = state;
        _availableView.enabled = _available;
        transform.localScale = _available ? Vector3.one * 1.2f : Vector3.one;
    }

    public void SetCompleted(bool value)
    {
        _icon.color = value ? Color.green : _defaultColor;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        _mapSystem.OnHoverNode(_index, true);
    }

    public void OnPointerExit(PointerEventData e)
    {
        _mapSystem.OnHoverNode(_index, false);
    }

    public void SetOnPointerColor(bool state)
    {
        _availableView.color = state ? Color.green : Color.white;
    }
}
