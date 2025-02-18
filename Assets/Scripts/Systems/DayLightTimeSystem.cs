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
        var missionLight = CurrentMissionInfo.Instance.GetCurrentMission().MissionView.MissionLight;
        Vector2 dayLightTime = missionLight.LightRotation;
        _light.transform.rotation = Quaternion.Euler(dayLightTime.x, dayLightTime.y, 0f);
        _light.colorTemperature = missionLight.Temperature;
        RenderSettings.ambientSkyColor = missionLight.SkyColor;
        RenderSettings.ambientEquatorColor = missionLight.EquatorColor;
        RenderSettings.ambientGroundColor = missionLight.GroundColor;
        _light.intensity = missionLight.Intencity;
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
