using System.Linq;
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
    [SerializeField] private GameObject _leftArrow;
    [SerializeField] private GameObject _rightArrow;

    private Resource _currentResource;


    public void ResetTraderPanel()
    {
        _currentResource = null;
        _quantsText.text = "0";
        _amountInputField.text = "1";
        _buttonImage.sprite = _buttonSprites[1];
        _quantsText.color = Colors.GreySeven;
        _amountInputField.gameObject.SetActive(false);
        _buyButton.interactable = false;
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

        if (string.IsNullOrEmpty(_amountInputField.text)) _amountInputField.text = "1";

        UpdateView();
    }

    public void OnAmountChange()
    {
        SanitizeAndClampInput();
        UpdateView();
    }

    private void UpdateView()
    {
        if (_currentResource == null)
        {
            _buyButton.interactable = false;
            if (_leftArrow) _leftArrow.SetActive(false);
            if (_rightArrow) _rightArrow.SetActive(false);
            _amountInputField.gameObject.SetActive(false);
            return;
        }

        _amountInputField.gameObject.SetActive(true);


        int amount = GetAmount(); // всегда валидное число 1..99
        int totalPrice = _currentResource.Price * amount;

        _quantsText.text = totalPrice.ToString();

        var enoughtQuants = _quantsSystem.GetQuants() >= totalPrice;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _buyButton.interactable = enoughtQuants;
        _quantsText.color = enoughtQuants ? Colors.GreySeven : Colors.WarningYellow;

        UpdateArrowsView(amount);
    }

    public void BuyResource()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        int amount = GetAmount();
        _quantsSystem.ChangeQuants(-_currentResource.Price * amount);
        _mainResources.ChangeResource(_currentResource.ResourceEnum, amount);
        _spaceSaveGame.SaveDataToJson();
        UpdateView();
    }

    // Читает число безопасно и держит диапазон 1..99
    private int GetAmount()
    {
        var text = _amountInputField.text;
        if (!int.TryParse(text, out int value)) value = 1;
        return Mathf.Clamp(value, 1, 99);
    }

    // Убирает всё, кроме цифр, и приводит к 1..99 без рекурсивных событий
    private void SanitizeAndClampInput()
    {
        string digitsOnly = new string((_amountInputField.text ?? "").Where(char.IsDigit).ToArray());

        if (string.IsNullOrEmpty(digitsOnly))
            digitsOnly = "1";

        // ограничим до 2 символов (99)
        if (digitsOnly.Length > 2)
            digitsOnly = digitsOnly.Substring(0, 2);

        if (!int.TryParse(digitsOnly, out int value))
            value = 1;

        value = Mathf.Clamp(value, 1, 99);

        // Без уведомления, чтобы не зациклить onValueChanged
        _amountInputField.SetTextWithoutNotify(value.ToString());
    }

    public void OnArrowChange(bool toRight)
    {
        int amount = Mathf.Clamp(GetAmount(), 1, 99);

        // Ничего не делаем, если уже на границе
        if (!toRight && amount <= 1)
        {
            _amountInputField.SetTextWithoutNotify("1");
            UpdateView();
            return;
        }
        if (toRight && amount >= 99)
        {
            _amountInputField.SetTextWithoutNotify("99");
            UpdateView();
            return;
        }

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        amount += toRight ? 1 : -1;
        _amountInputField.SetTextWithoutNotify(amount.ToString());
        UpdateView();
    }

    private void UpdateArrowsView(int amount)
    {
        if (_leftArrow) _leftArrow.SetActive(amount > 1);
        if (_rightArrow) _rightArrow.SetActive(amount < 99);
    }
}
