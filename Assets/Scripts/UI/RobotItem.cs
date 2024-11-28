using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RobotItem : MonoBehaviour
{
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private RobotInformation _robotInformation;
    [SerializeField] private RobotPanel _robotPanel;
    [SerializeField] private RobotSpawnerSystem _robotSpawnerSystem;
    private bool _isSelect;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backImage;

    [Header("Other")]
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    [SerializeField] private ResourcesView _robotResourcesView;
    private bool _resourcesEnough;
    public bool CanRepair() => _currentRobotSystem.HaveRobot() &&
                                !_currentRobotSystem.RobotDeath() &&
                                !_currentRobotSystem.RobotHealth().FullHealth() &&
                                _robotInformation.RobotType == RobotsData.Instance.GetRobotType();


    private void Start()
    {
        UpdateView();
        CustomEvents.OnRobotDie += UpdateViewAfterRobotDie;
        CustomEvents.OnRobotTakeDamage += UpdateView;
    }

    private void UpdateViewAfterRobotDie()
    {
        var time = WorldGameInfo.RobotDieDelay + WorldGameInfo.RobotDieDuration + 0.1f;
        _icon.color = Color.black;
        Invoke(nameof(SetButtonAndTextColor), time);
    }

    public void UpdateView()
    {
        _nameText.text = CanRepair() ? $"{Language.TextStatic[4]} {_robotInformation.Name[Language.LanguageNumber]}" : _robotInformation.Name[Language.LanguageNumber];
        _icon.sprite = _robotInformation.RobotSprite;
        if (_isSelect)
        {
            SetButtonAndTextColor();
            UpdateResourceCells();
        }
    }

    public void SelectView()
    {
        _robotPanel.DeselectAllRobotItems();
        SelectToggleState(true);
        UpdateResourceCells();
    }

    public void UpdateResourceCells()
    {
        if (!_currentRobotSystem.HaveRobot() || CanRepair())
        {
            _robotResourcesView.SetResourcesView(GetResources());
        }
        else _robotResourcesView.ResetCells();
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        _robotPanel.UpdateRobotInfo(_robotInformation);
        SetButtonAndTextColor();
    }

    public void SetButtonAndTextColor()
    {
        _resourcesEnough = _playerResources.ResourcesEnough(GetResources());
        _button.enabled = _currentRobotSystem.HaveRobot() ? _robotInformation.RobotType == RobotsData.Instance.GetRobotType() ? _currentRobotSystem.RobotHealth().FullHealth() || _currentRobotSystem.RobotHealth().IsDeath() ? false : _resourcesEnough : false : _resourcesEnough;
        _nameText.color = _resourcesEnough ? _isSelect ? Color.white : Colors.LightGrey : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = _currentRobotSystem.HaveRobot() ? _robotInformation.RobotType == RobotsData.Instance.GetRobotType() && !_currentRobotSystem.RobotHealth().IsDeath() ? Color.white : Color.black : _isSelect ? Color.white : Colors.LightGrey;
        _backImage.color = _isSelect ? Color.white : Colors.LightGrey;
    }

    public ResourcesForBuildWrapper[] GetResources()
    {
        if (CanRepair())
        {
            var robotHealth = _currentRobotSystem.RobotHealth();
            float healthPercentage = (float)(robotHealth.MaxHealth - robotHealth.CurrentHealth) / robotHealth.MaxHealth;

            return _robotInformation.ResourcesForBuild
                .Select(resource => new ResourcesForBuildWrapper
                {
                    ResourcesForBuild = resource.ResourcesForBuild,
                    RecourcesForBuildAmount = Mathf.CeilToInt(resource.RecourcesForBuildAmount * healthPercentage)
                })
                .ToArray();
        }

        return _robotInformation.ResourcesForBuild;
    }

    public void CreateOrRepairRobot()
    {
        _robotResourcesView.ResetCells();
        _playerResources.UseResourcesForBuilding(GetResources());


        if (CanRepair())
        {
            CustomEvents.FireRepairRobot();
            UpdateView();
        }
        else
        {
            _robotSpawnerSystem.SpawnRobot(_robotInformation.RobotType);
            _robotPanel.RefreshAllRobotItemsView();
            _robotPanel.UpdateDestroyButtonState();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnRobotDie -= UpdateViewAfterRobotDie;
        CustomEvents.OnRobotTakeDamage -= UpdateView;
    }
}
