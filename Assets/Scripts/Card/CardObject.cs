using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

public class CardObject : MonoBehaviour
{
    [Inject] private MissionResources _missionResources;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private TextMeshProUGUI _changeTextAmount;
    [SerializeField] private Transform _cardParentTransform;
    [SerializeField] private Button _changeCardButton;
    [SerializeField] private Tile _tile;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _tutorialView;
    [SerializeField] private Image _squareTutorialClickImage;
    private CardHolderSystem _cardHolderSystem;
    private bool _isSelect;
    public Tile GetTile() => _tile;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += TutorialHightlightCard;
        CustomEvents.OnTurnOffTutorialCardObjectView += TurnOffTutorialView;

        if (!_tutorialSystem.IsCompleteMissionTutorial())
        {
            TutorialHightlightCard(_tutorialSystem.GetTutorialStepEnum());
        }
    }

    private void TutorialHightlightCard(TutorialStepEnum tutorialStepEnum)
    {
        _tutorialView.SetActive(false);
        switch (tutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectBaseFoundationCard_10:
                _tutorialView.SetActive(_tile.GroundTileView == GroundTileViewEnum.BaseFoundation);
                _squareTutorialClickImage.enabled = true;
                break;
            case TutorialStepEnum.MissionSelectForestCard_31:
                _tutorialView.SetActive(_tile.GroundTileView == GroundTileViewEnum.Forest);
                _squareTutorialClickImage.enabled = true;
                break;
            case TutorialStepEnum.MissionAddCardsDescription_29:
                _tutorialView.SetActive(true);
                _squareTutorialClickImage.enabled = false;
                break;
            case TutorialStepEnum.MissionConstructionStoneMining_39:
                _tutorialView.SetActive(_tile.GroundTileView == GroundTileViewEnum.Mountain && !_tutorialSystem.IsCurrentInProcess());
                _squareTutorialClickImage.enabled = true;
                break;
            case TutorialStepEnum.MissionConstructionBallista_41:
                _tutorialView.SetActive(_tile.GroundTileView is GroundTileViewEnum.Plain or GroundTileViewEnum.Forest or GroundTileViewEnum.Desert or GroundTileViewEnum.Ground or GroundTileViewEnum.Highland && !_tutorialSystem.IsCurrentInProcess());
                _squareTutorialClickImage.enabled = true;
                break;
        }
    }

    // Отключаем у всех карт tutorialView
    public void TurnOffTutorialView()
    {
        _tutorialView.SetActive(false);
    }


    // Отключаем возможность выбора тайла в момент удаления карты
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
        if (!_tutorialSystem.CanSelectCardObject(_tile) || _isSelect) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);
        UpdateChangeTextColor();
        _cardHolderSystem.SelectCardInCardHolder(this);
        SelectViewToggle(true);
        _tutorialSystem.SelectCard(_tile.GroundTileView);
    }

    private void UpdateChangeTextColor()
    {
        var state = _missionResources.ResourceEnough(ResourceEnum.BeamEnergy, 1);
        _changeTextAmount.color = state ? Colors.GreyEight : Colors.WarningYellow;
    }

    public void SelectViewToggle(bool state)
    {
        _objectTransform.DOKill();

        if (_tile.GroundTileView != GroundTileViewEnum.BaseFoundation)
        {
            ChangeButtonToggle(state);
        }

        _isSelect = state;
        _objectTransform.DOAnchorPosY(state ? 30 : 0, 0.3f).SetUpdate(true);
    }

    private void ChangeButtonToggle(bool state)
    {
        _changeCardButton.interactable = state;
        _changeCardButton.enabled = state;
    }

    public void RandomChangeCard()
    {
        if (!_missionResources.ResourceEnough(ResourceEnum.BeamEnergy, 1))
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            _cardHolderSystem.CancelSelectCard();
            return;
        }

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.ChangeCard], transform.position);
        _missionResources.ChangeResource(ResourceEnum.BeamEnergy, -1);
        ChangeButtonToggle(false);

        // На всякий пожарный убьём предыдущие твины на контейнере
        _cardParentTransform.DOKill();

        float originalScaleX = _cardParentTransform.localScale.x;

        Sequence seq = DOTween.Sequence().SetUpdate(true);
        // Сжимаем по Х до 0
        seq.Append(_cardParentTransform.DOScaleX(0f, 0.2f).SetEase(Ease.InQuad));

        // Меняем карту на случайную доступную (кроме той же)
        seq.AppendCallback(() =>
        {
            var newTile = _cardHolderSystem.GetRandomAvailableCardExcept(_tile);
            if (newTile != null && newTile != _tile)
            {
                SetCardInfo(newTile, _cardHolderSystem);
                UpdateChangeTextColor();
            }
        });

        // Разворачиваем обратно
        seq.Append(_cardParentTransform.DOScaleX(originalScaleX, 0.2f).SetEase(Ease.OutQuad));

        seq.AppendCallback(() =>
        {
            ChangeButtonToggle(true);
        });
    }


    private void OnDestroy()
    {
        CustomEvents.OnStartTutorialStep -= TutorialHightlightCard;
        CustomEvents.OnTurnOffTutorialCardObjectView -= TurnOffTutorialView;

        if (_objectTransform != null)
        {
            _objectTransform.DOKill();
        }
    }
}
