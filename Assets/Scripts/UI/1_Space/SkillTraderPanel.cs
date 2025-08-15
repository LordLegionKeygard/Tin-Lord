using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class SkillTraderPanel : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private SkillTraderItem[] _skillTraderItems;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private TextMeshProUGUI _quantsText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite[] _buttonSprites;
    private SkillInfo _currentSkill;

    public void ResetTraderPanel()
    {
        _currentSkill = null;
        _quantsText.text = "0";
        _buttonImage.sprite = _buttonSprites[1];
        _buyButton.interactable = false;
        _quantsText.color = Colors.GreySeven;
        ResetToggleItems();

        for (int i = 0; i < _skillTraderItems.Length; i++)
        {
            _skillTraderItems[i].UpdateView(_skillTraderItems[i].GetSkillInfo(), _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.OpenedSkills[i], _spaceSaveGame.SpaceSaveData.Act);
        }
    }

    public void ResetToggleItems()
    {
        foreach (var item in _skillTraderItems)
        {
            item.SelectToggle(false);
        }
    }

    public void SelectSkill(SkillInfo skillInfo)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ResetToggleItems();

        _currentSkill = skillInfo;
        _skillTraderItems[(int)_currentSkill.SkillEnum].SelectToggle(true);

        UpdateView();
    }

    private void UpdateView()
    {
        _quantsText.text = _currentSkill.QuantPrice.ToString();

        var enoughtQuants = _quantsSystem.GetQuants() >= _currentSkill.QuantPrice;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _buyButton.interactable = enoughtQuants;
        _quantsText.color = enoughtQuants ? Colors.GreySeven : Colors.WarningYellow;
    }

    public void BuySkill()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Buy], transform.position);
        _quantsSystem.ChangeQuants(-_currentSkill.QuantPrice);
        _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.OpenedSkills[(int)_currentSkill.SkillEnum] = true;
        _spaceSaveGame.SaveDataToJson();
        ResetTraderPanel();
    }
}
