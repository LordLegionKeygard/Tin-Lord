using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourceTraderPanel : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private TMP_InputField _amountInputField;
    [SerializeField] private ResourceTraderItem[] _resourceTraderItems;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private MainResources _mainResources;
    [SerializeField] private TextMeshProUGUI _quantsText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite[] _buttonSprites;

    private Resource _currentResource;


    public void ResetTraderPanel()
    {
        _currentResource = null;
        _quantsText.text = "0";
        _amountInputField.text = "1";
        _buttonImage.sprite = _buttonSprites[1];
        _quantsText.color = Colors.GreySeven;
        _amountInputField.gameObject.SetActive(false);
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

        if (_amountInputField.text == string.Empty) return;
        UpdateView();
    }

    public void OnAmountChange()
    {
        if (_amountInputField.text == string.Empty) return;
        if (int.Parse(_amountInputField.text) == 0) _amountInputField.text = "1";
        if (int.Parse(_amountInputField.text) > 99) _amountInputField.text = "99";

        UpdateView();
    }

    private void UpdateView()
    {
        if (_currentResource == null) return;

        _amountInputField.gameObject.SetActive(true);
        var totalPrice = _currentResource.Price * int.Parse(_amountInputField.text);
        _quantsText.text = totalPrice.ToString();

        var enoughtQuants = _quantsSystem.GetQuants() >= totalPrice;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _buyButton.interactable = enoughtQuants;
        _quantsText.color = enoughtQuants ? Colors.GreySeven : Colors.WarningYellow;
    }

    public void BuyResource()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _quantsSystem.ChangeQuants(-_currentResource.Price * int.Parse(_amountInputField.text));
        _mainResources.ChangeResource(_currentResource.ResourceEnum, int.Parse(_amountInputField.text));
        _spaceSaveGame.SaveDataToJson();
        UpdateView();
    }
}
