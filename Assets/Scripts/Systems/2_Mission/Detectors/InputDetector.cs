using UnityEngine;
using Zenject;

public class InputDetector : MonoBehaviour
{
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [SerializeField] private ScrollViewInteraction _scrollViewInteraction;
    [SerializeField] private SkillTargetSystem _skillTargetSystem;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private TacticCardDetector _tacticCardDetector;
    public bool IsHaveCurrentSelectedTileObject() => _tileDetector.IsHaveCurrentSelectedTileObject() || _tacticCardDetector.IsHaveCurrentSelectedTileObject();

    public void InputOnTile()
    {
        if (_scrollViewInteraction.IsScrolling() || _skillTargetSystem.IsActive() || !_tutorialSystem.CanInputOnTile() || !_missionModeSystem.IsPlanetMode()) return;

        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            if (_cardHolderSystem.IsSelectedCardTile())
            {
                _tileDetector.InputOnTileForSetTileCard();
            }
            else
            {
                _tacticCardDetector.InputOnTileForSetTacticCard();
            }
        }
        else
        {
            _tileDetector.InputOnTile();
        }
    }

    public void ClearDetectors()
    {
        _tileDetector.ClearTileDetector();
        _tacticCardDetector.ClearTileDetector();
    }
}
