
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlider : BaseSlider
{
    [SerializeField] private Image _fill;

    public override void ChangeColor(Color newColor)
    {
        _fill.color = newColor;
    }
}
