using UnityEngine;
using FMODUnity;
using System.Collections;

public class MusicFade : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _eventEmitter;
    [SerializeField] private float _fadeDuration;

    private void Start()
    {
        CustomEvents.OnFadeOutAllMusic += FadeOutMusic;
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float newVolume = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);
            _eventEmitter.EventInstance.setVolume(newVolume);
            yield return null;
        }

        _eventEmitter.Stop();
    }

    private void OnDestroy()
    {
        CustomEvents.OnFadeOutAllMusic -= FadeOutMusic;
    }
}