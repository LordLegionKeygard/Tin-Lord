using UnityEngine;

public class TacticCardDetector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private UIPanelsMission _uiPanels;
    private TileObject _currentTileObject;
    private Transform _lastRayCastTransform;
    private TacticCard TacticCard => _cardHolderSystem.CurrentSelectedCard() as TacticCard;

    private void Update()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            var selected = _cardHolderSystem.CurrentSelectedCard();

            if (selected == null || selected.Kind != CardKind.Upgrade)
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
        switch (type)
        {
            case TacticCardType.IncreaseDamage:
                _currentTileObject.BuildingTileObject().TacticCardIncreaseDamageLevel();
                AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.TacticalCards[(int)TacticCardType.IncreaseDamage], transform.position);
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
        switch (upgrade.CardType)
        {
            case TacticCardType.IncreaseDamage:
                if (tileObject.BuildingTileObject().IsConstructionNow()) return false;
                return tileObject.BuildingTileObject().IsHaveTile() && tileObject.BuildingTileObject().GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.AttackingStructures;
        }

        return false;
    }

    private void ClearTileDetector()
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
