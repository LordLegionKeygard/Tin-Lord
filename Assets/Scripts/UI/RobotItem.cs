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
    private bool _isSelect;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backImage;

    [Header("Other")]
    [SerializeField] private PlayerSpawnerSystem _robotSpawnerSystem;
    [SerializeField] private ResourcesView _robotResourcesView;
    private bool _resourcesEnough;

    private void Start()
    {
        UpdateView();
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
        _robotResourcesView.SetBuildingResourcesView(GetResources());
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        SetTextColor();
    }

    private void SetTextColor()
    {
        _resourcesEnough = _playerResources.ResourcesForBuildEnough(GetResources());
        _button.enabled = _resourcesEnough;
        _nameText.color = _resourcesEnough ? _isSelect ? Color.white : Colors.LightGrey : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = _isSelect ? Color.white : Colors.LightGrey;
        _backImage.color = _isSelect ? Color.white : Colors.LightGrey;
        if (_isSelect) _robotResourcesView.SetBuildingResourcesView(GetResources());
    }

    public ResourcesForBuildWrapper[] GetResources()
    {
        if (_robotSpawnerSystem.HaveRobot() && !_robotSpawnerSystem.RobotDeath())
        {
            // Получаем здоровье робота
            var robotHealth = _robotSpawnerSystem.GetComponent<RobotHealth>();
            float healthPercentage = (float)(robotHealth.MaxHealth - robotHealth.CurrentHealth) / robotHealth.MaxHealth;

            // Пропорционально рассчитываем ресурсы для ремонта
            return _robotInformation.ResourcesForBuild
                .Select(resource => new ResourcesForBuildWrapper
                {
                    ResourcesForBuild = resource.ResourcesForBuild,
                    RecourcesForBuildAmount = Mathf.CeilToInt(resource.RecourcesForBuildAmount * healthPercentage)
                })
                .ToArray();
        }

        // Возвращаем ресурсы для строительства
        return _robotInformation.ResourcesForBuild;
    }
}
