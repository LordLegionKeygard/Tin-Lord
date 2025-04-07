using TMPro;
using UnityEngine;

public class EnemySlider : BaseSlider
{
    [SerializeField] private TextMeshProUGUI _levelText;
    public override void SetLevel(string level) => _levelText.text = level;
}
