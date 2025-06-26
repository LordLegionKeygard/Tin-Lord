using TMPro;
using UnityEngine;
using Zenject;

public class EnemySlider : BaseSlider
{
    [Inject] private EnemyDefenceSystem _enemyDefenceSystem;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject[] _defenceView;
    [SerializeField] private GameObject _miniBossView;

    public override void SetLevel(string level) => _levelText.text = level;

    public override void Start()
    {
        base.Start();
        CustomEvents.OnUpdateEnemySliderDefence += UpdateDefenceView;
    }

    private void OnEnable()
    {
        UpdateDefenceView(_enemyDefenceSystem.GetSliderValue());
    }

    public void UpdateDefenceView(int level)
    {
        for (int i = 0; i < _defenceView.Length; i++)
        {
            _defenceView[i].SetActive(i < level);
        }
    }

    public override void SetEnemySliderView(bool miniBoss)
    {
        _miniBossView.SetActive(miniBoss);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateEnemySliderDefence -= UpdateDefenceView;
    }
}
