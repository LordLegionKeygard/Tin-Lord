using TMPro;
using UnityEngine;
using Zenject;

public class EnemySlider : BaseSlider
{
    [Inject] private EcologySystem _ecologySystem;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _defenceText;
    public override void SetLevel(string level) => _levelText.text = level;


    public override void Start()
    {
        base.Start();
        CustomEvents.OnUpdateEnemyDefence += UpdateDefenceView;
        UpdateDefenceView();
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateEnemyDefence -= UpdateDefenceView;
    }

    public void UpdateDefenceView()
    {
        _defenceText.text = $"{_ecologySystem.GetTotalEcology()}%";
    }
}
