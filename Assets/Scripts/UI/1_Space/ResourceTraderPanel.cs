using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourceTraderPanel : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private ResourceTraderItem[] _resourceTraderItems;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private MainResources _mainResources;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite[] _buttonSprites;
    private Resource _currentResource;


    public void ResetTraderPanel()
    {
        _currentResource = null;
        _priceText.text = "0";
        _buttonImage.sprite = _buttonSprites[1];
        _priceText.color = Colors.GreySeven;
        ResetToggleItems();

        foreach (var item in _resourceTraderItems)
        {
            item.SetResourceOpen(_spaceSaveGame.SpaceSaveData.Act);
        }
    }

    private void ResetToggleItems()
    {
        foreach (var item in _resourceTraderItems)
        {
            item.SelectToggle(false);
        }
    }

    public void SelectResource(Resource resource)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ResetToggleItems();

        _currentResource = resource;
        _resourceTraderItems[(int)_currentResource.ResourceEnum].SelectToggle(true);

        UpdateView();
    }

    private void UpdateView()
    {
        _priceText.text = _currentResource.Price.ToString();

        var enoughtQuants = _quantsSystem.GetQuants() >= _currentResource.Price;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _buyButton.interactable = enoughtQuants;
        _priceText.color = enoughtQuants ? Colors.GreySeven : Colors.WarningYellow;
    }

    public void BuyResource()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _quantsSystem.ChangeQuants(-_currentResource.Price);
        _mainResources.ChangeResource(_currentResource.ResourceEnum, 1);
        _spaceSaveGame.SaveDataToJson();
        UpdateView();
    }
}
