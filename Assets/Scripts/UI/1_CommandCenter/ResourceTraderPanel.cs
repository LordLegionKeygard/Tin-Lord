using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourceTraderPanel : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private Resource[] _resources;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private MainResources _mainResources;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _resourceText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite[] _buttonSprites;
    private int _currentResource;


    public void PrepareTraderPanel()
    {
        _currentResource = 0;
        UpdateView();
    }

    public void ChangeResource(bool right)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        if (right) _currentResource++;
        else _currentResource--;

        if (_currentResource > _resources.Length - 1) _currentResource = 0;
        if (_currentResource < 0) _currentResource = _resources.Length - 1;

        UpdateView();
    }

    private void UpdateView()
    {
        _resourceText.text = Language.TextStatic[_resources[_currentResource].NameNumber];
        _image.sprite = _resources[_currentResource].Icon;
        _priceText.text = _resources[_currentResource].Price.ToString();

        var enoughtQuants = _quantsSystem.GetQuants() >= _resources[_currentResource].Price;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _buyButton.interactable = enoughtQuants;
        _priceText.color = enoughtQuants ? Color.white : Colors.WarningYellow;
    }

    public void BuyResource()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _quantsSystem.ChangeQuants(-_resources[_currentResource].Price);
        _mainResources.ChangeResource(_resources[_currentResource].ResourceEnum, 1);
        _commandCenterSaveGame.SaveGameData(false);
        UpdateView();
    }
}
