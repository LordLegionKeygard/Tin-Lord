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
    [SerializeField] private RobotsData _robotsData;
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
    private bool _repair;

    private void Start()
    {
        UpdateView();
        CustomEvents.OnRobotDie += UpdateViewAfterRobotDie;
    }

    private void UpdateViewAfterRobotDie()
    {
        var time = WorldGameInfo.RobotDieDelay + WorldGameInfo.RobotDieDuration + 0.1f;
        Invoke(nameof(SetButtonAndTextColor), time);
    }

    public void UpdateView()
    {
        _nameText.text = _robotInformation.Name[Language.LanguageNumber];
        _icon.sprite = _robotInformation.RobotSprite;
    }

    public void SelectView()
    {
        _robotPanel.UnselectAllRobots();

        SelectToggleState(true);

        if (!_currentRobotSystem.HaveRobot() ||
        (_currentRobotSystem.HaveRobot() && !_currentRobotSystem.RobotDeath() &&
        !_currentRobotSystem.RobotHealth().FullHealth() && _robotInformation.RobotType == _robotsData.GetRobotType()))
        {
            _robotResourcesView.SetResourcesView(GetResources());
        }
        else _robotResourcesView.ResetCells();

    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        _robotPanel.UpdateTexts(_robotInformation);
        SetButtonAndTextColor();
    }

    private void SetButtonAndTextColor()
    {
        _resourcesEnough = _playerResources.ResourcesEnough(GetResources());
        _button.enabled = _currentRobotSystem.HaveRobot() ? _robotInformation.RobotType == _robotsData.GetRobotType() ? _currentRobotSystem.RobotHealth().FullHealth() || _currentRobotSystem.RobotHealth().IsDeath() ? false : _resourcesEnough : false : _resourcesEnough;
        _nameText.color = _resourcesEnough ? _isSelect ? Color.white : Colors.LightGrey : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = _currentRobotSystem.HaveRobot() ? _robotInformation.RobotType == _robotsData.GetRobotType() ? Color.white : Color.black : _isSelect ? Color.white : Colors.LightGrey;
        _backImage.color = _isSelect ? Color.white : Colors.LightGrey;
        // if (_isSelect) _robotResourcesView.SetBuildingResourcesView(GetResources());
    }

    public ResourcesForBuildWrapper[] GetResources()
    {
        _repair = _currentRobotSystem.HaveRobot() && !_currentRobotSystem.RobotDeath();

        if (_repair)
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


        if (_repair)
        {
            CustomEvents.FireRepairRobot();
        }
        else
        {
            _robotSpawnerSystem.SpawnRobot(_robotInformation.RobotType);
            _robotPanel.UnselectAllRobots();
            _robotPanel.UpdateDestroyButton();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnRobotDie -= UpdateViewAfterRobotDie;
    }
}
