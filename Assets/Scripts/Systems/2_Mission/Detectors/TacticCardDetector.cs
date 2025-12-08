using UnityEngine;
using Zenject;

public class TacticCardDetector : MonoBehaviour
{
    [Inject] private TextViewSpawner _textViewSpawner;
    [Inject] private MissionResources _missionResources;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private UIPanelsMission _uiPanels;
    private TileObject _currentTileObject;
    private Transform _lastRayCastTransform;
    private TacticCard TacticCard => _cardHolderSystem.CurrentSelectedCard() as TacticCard;

    public bool IsHaveCurrentSelectedTileObject() => _currentTileObject == null ? false : true;

    private void Update()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            var selected = _cardHolderSystem.CurrentSelectedCard();

            if (selected == null || selected.Kind != CardKind.Tactic)
            {
                if (_currentTileObject != null) ClearTileDetector();
                return;
            }

            if (IsPointerOverUISystem.IsPointerOverUI)
            {
                ClearTileDetector();
                return;
            }

            RaycastHit raycastHit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 500f, _layerMask))
            {
                if (raycastHit.transform != null)
                {
                    if (_lastRayCastTransform == raycastHit.transform)
                    {
                        return;
                    }
                    _lastRayCastTransform = raycastHit.transform;

                    SelectTile(raycastHit.transform.gameObject);
                }
            }
        }
    }

    public void InputOnTileForSetTacticCard()
    {
        var card = TacticCard;
        if (card == null) return;

        if (_currentTileObject == null) return;

        bool isValid = IsValidTarget(card, _currentTileObject);
        if (!isValid)
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }

        UseTacticCard(card.CardType);
        ClearTileDetector();
        _cardHolderSystem.RemoveCurrentCard();
    }

    private void UseTacticCard(TacticCardType type)
    {
        var rarity = _cardHolderSystem.GetCurrentSelectCardObjectRarity();
        Vector3 pos = _currentTileObject.transform.position + Vector3.up * 0.3f;
        if (type != TacticCardType.ChangeRarity) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.TacticalCards[(int)type], transform.position);
        switch (type)
        {
            case TacticCardType.IncreaseDamage:
                _currentTileObject.BuildingTileObject().TacticCardIncreaseDamageLevel(rarity);
                _textViewSpawner.ShowTextView(pos, Language.TextStatic[62], Colors.LightGreen);
                break;
            case TacticCardType.IncreaseDurability:
                _currentTileObject.BuildingTileObject().TacticCardIncreaseHealthLevel(rarity);
                _textViewSpawner.ShowTextView(pos, Language.TextStatic[227], Colors.LightGreen);
                break;
            case TacticCardType.Repair:
                var healPercent = 0.2f;
                _currentTileObject.BuildingHealth().PercentRepair(rarity * healPercent);
                _textViewSpawner.ShowTextView(pos, Language.TextStatic[238], Colors.LightGreen);
                break;
            case TacticCardType.OverProduction:
                var resource = _currentTileObject.GetCurrentResourceProduction();
                var count = 0;
                switch (resource.ResourceType)
                {
                    case ResourceType.Resource:
                        count = 8;
                        break;
                    case ResourceType.Material:
                        count = 4;
                        break;
                    case ResourceType.Component:
                        count = 2;
                        break;
                    case ResourceType.Other:
                        count = 10;
                        break;
                }
                var totalCount = count * rarity;
                _missionResources.ChangeResource(resource.ResourceEnum, totalCount);
                _textViewSpawner.ShowAddResourceView(pos, resource.Icon, totalCount);
                break;
            case TacticCardType.ChangeRarity:
                var rnd = Random.Range(0, 100);
                var success = rnd <= WorldGameInfo.TacticCardChangeSuccessRarityChance + rarity * 10;
                switch (_currentTileObject.GetRarity())
                {
                    case 1:
                        _currentTileObject.SetRarity(success ? 2 : 1);
                        break;
                    case 2:
                        _currentTileObject.SetRarity(success ? 3 : 1);
                        break;
                    case 3:
                        _currentTileObject.SetRarity(success ? 4 : 2);
                        break;
                    case 4:
                        _currentTileObject.SetRarity(success ? 5 : 3);
                        break;
                }

                AudioManager.Instance.PlayerOneShot(success ? FMODEvents.Instance.ChangeRaritySuccess : FMODEvents.Instance.ChangeRarityFailure, transform.position);
                _textViewSpawner.ShowTextView(pos, Language.TextStatic[success ? 236 : 237], success ? Colors.LightGreen : Colors.WarningRed);
                _currentTileObject.UpdateResourceModifier();
                break;
        }
    }

    private void SelectTile(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();

        if (newTileObject.IsGroundDestroyedNow() || newTileObject.IsBuildingDestroyedNow()) return;


        UnselectLastTile(true);
        _currentTileObject = newTileObject;

        var card = TacticCard;

        bool valid = IsValidTarget(card, _currentTileObject);
        _currentTileObject.GroundTileObject().SelectTile(true, valid ? SelectTileEnum.EmptyTileSelect : SelectTileEnum.ErrorSelect);
    }

    private bool IsValidTarget(TacticCard upgrade, TileObject tileObject)
    {
        var groundTileObject = tileObject.GroundTileObject();
        var isHaveGroundTile = groundTileObject.IsHaveTile();
        var buildingTileObject = tileObject.BuildingTileObject();
        var isHaveBuildingTile = buildingTileObject.IsHaveTile();
        var isConstructionNow = buildingTileObject.IsConstructionNow();
        switch (upgrade.CardType)
        {
            case TacticCardType.IncreaseDamage:
                if (isConstructionNow) return false;
                return isHaveBuildingTile && buildingTileObject.GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.AttackingStructures;
            case TacticCardType.IncreaseDurability:
                if (isConstructionNow) return false;
                return isHaveBuildingTile && buildingTileObject.GetCurrentBuildingTile().BuildingTileView != BuildingTileViewEnum.Base;
            case TacticCardType.Repair:
                if (isConstructionNow) return false;
                return isHaveBuildingTile && !tileObject.BuildingHealth().IsFullHealth();
            case TacticCardType.OverProduction:
                if (isConstructionNow) return false;
                return isHaveBuildingTile && tileObject.GetCurrentResourceProduction() != null;
            case TacticCardType.ChangeRarity:
                return isHaveGroundTile && groundTileObject.CurrentGroundTile().GroundTileView != GroundTileViewEnum.BaseFoundation && tileObject.GetRarity() != (int)CardRarityEnum.Legendary;
        }

        return false;
    }

    public void ClearTileDetector()
    {
        if (_currentTileObject != null)
        {
            _currentTileObject.GroundTileObject().SelectTile(false);
            _currentTileObject = null;
        }
        _lastRayCastTransform = null;
    }

    public void UnselectLastTile(bool isResetMainPanels)
    {
        if (_currentTileObject != null)
        {
            _currentTileObject.GroundTileObject().SelectTile(false);

            if (isResetMainPanels)
            {
                _selectTilePanel.ResetPanels();
                _uiPanels.MainPanelsViewToggle(false, false);
            }
        }
    }
}
