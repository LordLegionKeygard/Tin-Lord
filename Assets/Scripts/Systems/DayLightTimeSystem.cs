using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightTimeSystem : MonoBehaviour
{
    [SerializeField] private Transform _directionalLight;
    [SerializeField] [ColorUsage(true, true)] private Color _defaultAmbientColor;

    private void Start()
    {
        CustomEvents.OnDataLoad += SetDayLightTime;
    }

    public void SetDayLightTime()
    {
        var missionLight = CurrentMissionInfo.Instance.GetCurrentMission().MissionLight;
        Vector2 dayLightTime = missionLight.LightRotation;
        _directionalLight.rotation = Quaternion.Euler(dayLightTime.x, dayLightTime.y, 0f);
        RenderSettings.ambientLight = missionLight.AmbientColor;
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
