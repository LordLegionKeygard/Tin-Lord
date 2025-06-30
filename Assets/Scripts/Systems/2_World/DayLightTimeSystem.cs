using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightTimeSystem : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] [ColorUsage(true, true)] private Color _defaultAmbientColor;

    private void Start()
    {
        CustomEvents.OnDataLoad += SetDayLightTime;
    }

    public void SetDayLightTime()
    {
        var landscapeLight = CurrentMissionInfo.Instance.GetCurrentLandscape().MissionView.MissionLight;
        Vector2 dayLightTime = landscapeLight.LightRotation;
        _light.transform.rotation = Quaternion.Euler(dayLightTime.x, dayLightTime.y, 0f);
        _light.colorTemperature = landscapeLight.Temperature;
        RenderSettings.ambientSkyColor = landscapeLight.SkyColor;
        RenderSettings.ambientEquatorColor = landscapeLight.EquatorColor;
        RenderSettings.ambientGroundColor = landscapeLight.GroundColor;
        _light.intensity = landscapeLight.Intencity;
    }

    private void ResetLight()
    {
       RenderSettings.ambientLight = _defaultAmbientColor; 
    }

    private void OnDestroy()
    {
        ResetLight();
        CustomEvents.OnDataLoad -= SetDayLightTime;
    }
}
