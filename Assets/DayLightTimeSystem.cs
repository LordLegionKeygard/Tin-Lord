using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightTimeSystem : MonoBehaviour
{
    [SerializeField] private Transform _directionalLight;
    [SerializeField] private LightTimeInfo _lightTimeInfo;

    private void Start()
    {
        CustomEvents.OnDataLoad += SetDayLightTime;
    }

    public void SetDayLightTime()
    {
        Vector2 dayLightTime = _lightTimeInfo.DayLightTimeWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().DayLightTime].LightRotation;
        _directionalLight.rotation = Quaternion.Euler(dayLightTime.x, dayLightTime.y, 0f);
    }

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetDayLightTime;
    }
}
