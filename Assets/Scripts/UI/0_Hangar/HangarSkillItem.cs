using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HangarSkillItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private HangarSystem _hangarSystem;
    [SerializeField] private SkillInfo _skillInfo;
    private bool _isOpen;
    private bool _isSelect;
    public bool IsOpen() => _isOpen;
    public bool IsSelect() => _isSelect;
    public SkillInfo GetInfo() => _skillInfo;

    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectView;
    private Image _backImage;
    private int _selectSkillIndex = -1;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
    }

    public void UpdateView()
    {
        _selectView.enabled = _isSelect;
        _priceText.gameObject.SetActive(!_isOpen);
        _icon.enabled = _isOpen;
        _icon.sprite = _skillInfo.Icon;
        _priceText.text = _skillInfo.ShardPrice.ToString();
        _selectView.color = _isOpen && _isSelect ? Colors.LightGreen : Colors.GreyFive;

        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        _priceText.color = _hangarSystem.EnoughtShards(_skillInfo.ShardPrice) ? Color.white : Colors.WarningYellow;
    }

    public void SelectButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        if (_isSelect)
        {
            _hangarSystem.UnselectSkill(_selectSkillIndex);
        }
        else
        {
            _hangarSystem.SelectSkill(_skillInfo.SkillEnum, _isOpen);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hangarSystem.SetSkillDescription(_skillInfo.SkillEnum, _isOpen, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hangarSystem.SetSkillDescription(_skillInfo.SkillEnum, _isOpen, true);
    }

    public void SelectToggleState(bool state, int selectSkillIndex)
    {
        _selectSkillIndex = selectSkillIndex;
        _isSelect = state;
        UpdateView();
    }

    public void SetIsOpen(bool state)
    {
        _isOpen = state;
        UpdateView();
    }

}
