using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class SkillTraderPanel : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite[] _buttonSprites;
    private int _currentSkill;
    [SerializeField] private SkillInfo[] _skills;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _skillIcons;
    [SerializeField] private GameObject[] _selectObjects;

    public void PrepareTraderPanel()
    {
        _currentSkill = -1;
        UpdateView();
    }

    public void ChangeSkill(int id)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _currentSkill = id;
        UpdateView();
    }

    private void UpdateView()
    {
        SelectView();
        for (int i = 0; i < _buttons.Length; i++)
        {
            var skillOpen = _spaceSaveGame.SpaceSaveData.OpenedSkills[i];
            _buttons[i].interactable = !skillOpen;
            _skillIcons[i].color = skillOpen ? Colors.AlphaGreySeven : Colors.GreySeven;
        }

        _priceText.text = _currentSkill == -1 ? "0" : _skills[_currentSkill].QuantPrice.ToString();
        var enoughtQuants = _currentSkill == -1 ? false : _quantsSystem.GetQuants() >= _skills[_currentSkill].QuantPrice;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _buyButton.interactable = enoughtQuants;
        _priceText.color = enoughtQuants ? Color.white : Colors.WarningYellow;
    }

    public void BuySkill()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _quantsSystem.ChangeQuants(-_skills[_currentSkill].QuantPrice);
        _spaceSaveGame.SpaceSaveData.OpenedSkills[_currentSkill] = true;
        _spaceSaveGame.SaveDataToJson();
        UpdateView();
        _currentSkill = -1;
    }

    private void SelectView()
    {
        foreach (var item in _selectObjects)
        {
            item.SetActive(false);
        }

        if (_currentSkill == -1) return;
        _selectObjects[_currentSkill].SetActive(true);
    }
}
