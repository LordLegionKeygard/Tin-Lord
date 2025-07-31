using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _tutorialView;
    private CardHolderSystem _cardHolderSystem;
    public Tile GetTile() => _tile;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += TutorialHightlightCard;
    }

    private void TutorialHightlightCard(TutorialStepEnum tutorialStepEnum)
    {
        switch (tutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectBaseFoundationCard_10:
                _tutorialView.SetActive(_tile.GroundTileView == GroundTileViewEnum.BaseFoundation);
                break;
        }
    }

    private void CompleteTutorialStepSelectCard()
    {
        _tutorialView.SetActive(false);
        switch (_tile.GroundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionSelectBaseFoundationCard_10);
                break;
        }
    }

    public void DisabledButton()
    {
        _button.enabled = false;
    }

    public void SetCardInfo(Tile tile, CardHolderSystem cardHolderSystem)
    {
        _tile = tile;
        _cardHolderSystem = cardHolderSystem;
        gameObject.name = _tile.Name[0];
        _image.sprite = _tile.Icon;
        _text.text = _tile.Name[Language.LanguageNumber];
    }

    public void SelectCardObject()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);
        _cardHolderSystem.SelectCardInCardHolder(this);
        CardObjectViewToggle(true);
        CompleteTutorialStepSelectCard();
    }

    public void CardObjectViewToggle(bool state)
    {
        if (_objectTransform == null) return;

        _objectTransform.DOKill();

        if (state) _objectTransform.DOAnchorPosY(42, 0.3f).SetUpdate(true);
        else _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
    }

    private void OnDestroy()
    {
        CustomEvents.OnStartTutorialStep -= TutorialHightlightCard;

        if (_objectTransform != null)
        {
            _objectTransform.DOKill();
        }
    }
}
