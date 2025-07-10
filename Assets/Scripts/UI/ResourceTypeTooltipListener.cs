using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceTypeTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_image.sprite == null) return;
        CustomEvents.FireTooltipToggle(true, 0);
        if (_map.TryGetValue(_image.sprite.name, out int index))
        {
            CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, Language.TextStatic[index], 0.5f, WorldGameInfo.ResourcePivot);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }

    private readonly Dictionary<string, int> _map = new()
    {
        { "0_Wood_Icon", 153 },
        { "1_Stone_Icon", 154 },
        { "2_IronOre_Icon", 155 },
        { "3_CopperOre_Icon", 156 },
        { "4_Coal_Icon", 157 },
        { "5_Oil_Icon", 158 },
        { "6_Water_Icon", 159 },
        { "7_Sand_Icon", 160 },
        { "8_Electricity_Icon", 161 },
        { "9_StoneBlock_Icon", 162 },
        { "10_IronIngot_Icon", 163 },
        { "11_SteelIngot_Icon", 164 },
        { "12_CopperPlate_Icon", 165 },
        { "13_Concrete_Icon", 166 },
        { "14_Steam_Icon", 167 },
        { "15_Glass_Icon", 168 },
        { "16_CopperWire_Icon", 169 },
        { "17_GearWheel_Icon", 170 },
        { "18_ElectronicCircuit_Icon", 171 },
        { "19_Processor_Icon", 172 },
        { "20_Engine_Icon", 173 },
        { "21_ElectricEngine_Icon", 174 },
        { "22_DataFragment", 175 },
        { "23_BeamEnergy", 176 }
    };
}
