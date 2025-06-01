using TMPro;
using UnityEngine;
using Zenject;

public class EnemySlider : BaseSlider
{
    [Inject] private EnemyDefenceSystem _enemyDefenceSystem;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject[] _defenceView;

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

    private void OnDestroy()
    {
        CustomEvents.OnUpdateEnemySliderDefence -= UpdateDefenceView;
    }

    public void UpdateDefenceView(int level)
    {
        for (int i = 0; i < _defenceView.Length; i++)
        {
            _defenceView[i].SetActive(i < level);
        }
    }
}
