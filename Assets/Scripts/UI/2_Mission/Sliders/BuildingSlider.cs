
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlider : BaseSlider
{
    [SerializeField] private Image _fill;
    [SerializeField] private GameObject _tutorialArrow;

    public override void Start()
    {
        base.Start();
        CustomEvents.OnStartTutorialStep += ActiveTutorialArrow;
    }

    public override void ChangeColor(Color newColor)
    {
        _fill.color = newColor;
    }

    private void ActiveTutorialArrow(TutorialStepEnum tutorialStepEnum)
    {
        _tutorialArrow.SetActive(tutorialStepEnum == TutorialStepEnum.MissionBuildingTakeDamage_56);
    }

    private void OnDestroy()
    {
        CustomEvents.OnStartTutorialStep -= ActiveTutorialArrow;
    }
}
