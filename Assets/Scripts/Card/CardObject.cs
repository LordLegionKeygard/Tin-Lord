using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

public class CardObject : MonoBehaviour
{
    [Inject] private readonly RarityCardsSystem _rarityCardsSystem;
    [Inject] private MissionResources _missionResources;
    [Inject] private readonly TutorialSystem _tutorialSystem;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _changeTextAmount;
    [SerializeField] private RectTransform _cardParentTransform;
    [SerializeField] private Button _changeCardButton;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _mainCardTransform;
    [SerializeField] private Button _button;

    [Header("Tutorial")]
    [SerializeField] private GameObject _tutorialView;
    [SerializeField] private Image _squareTutorialClickImage;

    private CardHolderSystem _cardHolderSystem;
    private bool _isSelect;
    [SerializeField] private Card _card;
    [SerializeField] private int _rarity;

    public Card GetCard() => _card;
    public Tile GetTile() => _card as Tile;
    public int GetRarity() => _rarity;

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
        _squareTutorialClickImage.enabled = false;

        var tile = GetTile();
        if (tile == null) return;

        switch (tutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectBaseFoundationCard_10:
                _tutorialView.SetActive(tile.GroundTileView == GroundTileViewEnum.BaseFoundation);
                _squareTutorialClickImage.enabled = true;
                break;
            case TutorialStepEnum.MissionSelectForestCard_31:
                _tutorialView.SetActive(tile.GroundTileView == GroundTileViewEnum.Forest);
                _squareTutorialClickImage.enabled = true;
                break;
            case TutorialStepEnum.MissionAddCardsDescription_29:
                _tutorialView.SetActive(true);
                _squareTutorialClickImage.enabled = false;
                break;
            case TutorialStepEnum.MissionConstructionStoneMining_39:
                _tutorialView.SetActive(tile.GroundTileView == GroundTileViewEnum.Mountain && !_tutorialSystem.IsCurrentInProcess());
                _squareTutorialClickImage.enabled = true;
                break;
            case TutorialStepEnum.MissionConstructionBallista_41:
                _tutorialView.SetActive(tile.GroundTileView is GroundTileViewEnum.Plain or GroundTileViewEnum.Forest or GroundTileViewEnum.Desert or GroundTileViewEnum.Ground or GroundTileViewEnum.Highland && !_tutorialSystem.IsCurrentInProcess());
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

    public void SetCardInfo(Card card, CardHolderSystem holder, int rarity)
    {
        _card = card;
        _rarity = rarity;
        _cardHolderSystem = holder;

        gameObject.name = _card.Name[0];
        _image.sprite = _card.Icon;
        _text.text = _card.Name[Language.LanguageNumber];
        _text.color = _rarityCardsSystem.GetRarityColor(_rarity);
    }

    public void SelectCardObject()
    {
        // Если уже выделена — ничего не делаем
        if (_isSelect) return;

        var tile = GetTile(); // _card as Tile

        if (tile != null)
        {
            // === Ветвь для тайла ===
            if (!_tutorialSystem.CanSelectCardObject(tile)) return;

            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);

            // Индикатор энергии нужен только для смены тайлов
            UpdateChangeTextColorForTile();

            _cardHolderSystem.SelectCardInCardHolder(this);
            SelectViewToggle(true);

            // Тутаориал ждёт GroundTileView
            _tutorialSystem.SelectCard(tile.GroundTileView);
            return;
        }

        // === Ветвь для апгрейда (или любых других не-тайловых карт) ===
        // Если у тебя нет отдельной проверки туториала для апгрейдов — просто разрешаем выделение.
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);

        _cardHolderSystem.SelectCardInCardHolder(this);
        SelectViewToggle(true);
    }

    private void UpdateChangeTextColorForTile()
    {
        if (_changeTextAmount == null) return;
        // Этот индикатор имеет смысл только для тайлов (смена стоит 1 энергию)
        bool enough = _missionResources.ResourceEnough(ResourceEnum.BeamEnergy, 1);
        _changeTextAmount.color = enough ? Colors.GreyEight : Colors.WarningYellow;
    }


    private void UpdateChangeTextColor()
    {
        var state = _missionResources.ResourceEnough(ResourceEnum.BeamEnergy, 1);
        _changeTextAmount.color = state ? Colors.GreyEight : Colors.WarningYellow;
    }

    public void SelectViewToggle(bool state)
    {
        _mainCardTransform.DOKill();

        var tile = GetTile();
        if (tile != null && tile.GroundTileView != GroundTileViewEnum.BaseFoundation)
        {
            ChangeButtonToggle(state);
            _isSelect = state;
            _mainCardTransform.DOAnchorPosY(state ? 30 : 0, 0.3f).SetUpdate(true);
        }
        else
        {
            ChangeButtonToggle(false);
            _isSelect = state;
            _cardParentTransform.DOAnchorPosY(state ? 30 : 0, 0.3f).SetUpdate(true);
        }

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
            var newTile = _cardHolderSystem.GetRandomAvailableCardExcept(_card);
            if (newTile != null && newTile != _card)
            {
                SetCardInfo(newTile, _cardHolderSystem, _rarityCardsSystem.GetRarity());
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

        if (_mainCardTransform != null)
        {
            _mainCardTransform.DOKill();
        }
    }
}
