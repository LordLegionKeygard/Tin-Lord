using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLightsTimer : BaseLight
{
    [SerializeField] private float _timeToUnlight;

    private IEnumerator Start()
    {
        float elapsed = 0f;

        while (elapsed < _timeToUnlight)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        ChangeIntensity();
    }
}
