using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

public class CardObject : MonoBehaviour
{
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private Tile _tile;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _tutorialView;
    [SerializeField] private Image _squareTutorialClickImage;
    private CardHolderSystem _cardHolderSystem;
    public Tile GetTile() => _tile;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += TutorialHightlightCard;
        CustomEvents.OnCompleteTutorialStep += DisableTutorialView;
    }

    private void TutorialHightlightCard(TutorialStepEnum tutorialStepEnum)
    {
        switch (tutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectBaseFoundationCard_10:
                _tutorialView.SetActive(_tile.GroundTileView == GroundTileViewEnum.BaseFoundation);
                break;
            case TutorialStepEnum.MissionSelectForestCard_31:
                _tutorialView.SetActive(_tile.GroundTileView == GroundTileViewEnum.Forest);
                break;
            case TutorialStepEnum.MissionAddCardsDescription_29:
                _tutorialView.SetActive(true);
                _squareTutorialClickImage.enabled = false;
                break;
        }
    }

    private void DisableTutorialView(TutorialStepEnum tutorialStepEnum)
    {
        if (tutorialStepEnum == TutorialStepEnum.MissionToggleOffSettlement_30)
        {
            _tutorialView.SetActive(false);
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
        if (!_tutorialSystem.CanSelectCardObject(_tile)) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);
        _cardHolderSystem.SelectCardInCardHolder(this);
        CardObjectViewToggle(true);
        _tutorialView.SetActive(false);
        _squareTutorialClickImage.enabled = true;
        _tutorialSystem.SelectCard(_tile.GroundTileView);
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
        CustomEvents.OnCompleteTutorialStep -= DisableTutorialView;

        if (_objectTransform != null)
        {
            _objectTransform.DOKill();
        }
    }
}
