using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRenderSettings : MonoBehaviour
{
    [SerializeField] private CRTFilter.CRTRendererFeature _crtRendererFeature;

    public void UpdateRenderSettings(int ecology)
    {
        float normalizedEcology = Mathf.Clamp01(ecology / -100f);

        _crtRendererFeature.vignetteSmooth = Mathf.Lerp(3, 20, normalizedEcology);
        _crtRendererFeature.vignetteRound = Mathf.Lerp(48, 3.5f, normalizedEcology);
        _crtRendererFeature.shadowlinesAlpha = Mathf.Lerp(0f, 0.05f, normalizedEcology);
        _crtRendererFeature.noiseAlpha = Mathf.Lerp(0f, 0.035f, normalizedEcology);
        _crtRendererFeature.colorize = Mathf.Lerp(1, 0, normalizedEcology);
    }
}
