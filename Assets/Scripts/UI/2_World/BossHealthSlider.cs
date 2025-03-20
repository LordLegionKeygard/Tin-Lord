using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BossHealthSlider : MonoBehaviour
{
    public static BossHealthSlider Instance;
    [SerializeField] private GameObject _bossHealthView;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _backSlider;
    // [SerializeField] private TextMeshProUGUI _tmp;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.Log("Two or more instance");
        }
        Instance = this;
    }

    // public void SetBossName(string name)
    // {
    //     _tmp.text = name;
    // }

    public void UpdateSliders(float health)
    {
        _slider.value = health;
        _backSlider.DOValue(health, 1f);
    }

    public void SetMaxHealth(float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;

        _backSlider.maxValue = maxHealth;
        _backSlider.value = maxHealth;
    }

    public void ActivateSlider(bool state)
    {
        _bossHealthView.gameObject.SetActive(state);

        // if (state) CustomEvents.FireTurnOffLevelMusic();
        // else CustomEvents.FirePlayRandomLevelMusic();
    }
}
