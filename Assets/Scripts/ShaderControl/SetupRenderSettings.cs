using UnityEngine;

public class SetupRenderSettings : MonoBehaviour
{
    [SerializeField] private CRTFilter.CRTRendererFeature _crtRendererFeature;

    private void Awake()
    {
        ResetRender();
    }

    private void ResetRender()
    {
        _crtRendererFeature.screenBend = 0;
        _crtRendererFeature.vignetteSize = 0;
        _crtRendererFeature.shadowlines = 50;
        _crtRendererFeature.shadowlinesSpeed = -5;
        _crtRendererFeature.shadowlinesAlpha = 0;
        _crtRendererFeature.noiseAlpha = 0;
        _crtRendererFeature.colorize = 1;
    }

    public void SetTerminalRender()
    {
        _crtRendererFeature.screenBend = 6;
        _crtRendererFeature.vignetteSize = 5.7f;
        _crtRendererFeature.vignetteSmooth = 1;
        _crtRendererFeature.vignetteRound = 38;
        _crtRendererFeature.shadowlines = 30;
        _crtRendererFeature.shadowlinesSpeed = -3;
        _crtRendererFeature.shadowlinesAlpha = 0.001f;
        _crtRendererFeature.noiseAlpha = 0;
        _crtRendererFeature.colorize = 1;
    }

    public void UpdateEcologyRender(int ecology)
    {
        float normalizedEcology = Mathf.Clamp01(ecology / -100f);

        _crtRendererFeature.screenBend = 6;
        _crtRendererFeature.vignetteSize = 5.7f;
        _crtRendererFeature.vignetteSmooth = Mathf.Lerp(3, 20, normalizedEcology);
        _crtRendererFeature.vignetteRound = Mathf.Lerp(48, 3.5f, normalizedEcology);
        // _crtRendererFeature.noiseAlpha = Mathf.Lerp(0f, 0.035f, normalizedEcology);
        float shadowlinesEcology = Mathf.Clamp01((ecology + 50) / -50f);
        _crtRendererFeature.shadowlinesAlpha = shadowlinesEcology * 0.015f;

        if (ecology < -25)
        {
            _crtRendererFeature.colorize = Mathf.Lerp(1, 0, normalizedEcology/ 2);
        }
        else
        {
            _crtRendererFeature.colorize = 1;
        }
    }

    private void OnDisable()
    {
        ResetRender();
    }
}
