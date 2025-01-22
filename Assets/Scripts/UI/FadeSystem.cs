using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    [SerializeField] private GameObject _fade;
    [SerializeField] private Image _fadeImage;

    private void Start()
    {
        CustomEvents.OnFade += Fade;
    }

    public void Fade(FadeType fadeType)
    {
        switch (fadeType)
        {
            case FadeType.StartFade:
                StartFade(0);
                break;
            case FadeType.StartFadeSlow:
                StartFade(2);
                break;
            case FadeType.FadeOut:
                FadeOut(1);
                break;
            case FadeType.FadeOutPrologue:
                FadeOut(4);
                break;
        }
    }

    private void StartFade(float time)
    {
        _fade.SetActive(true);
        DOVirtual.DelayedCall(time, () =>
        {
            _fadeImage.DOFade(1f, 0.15f).SetEase(Ease.InOutQuad).SetUpdate(true);
        });
    }

    private void FadeOut(float time)
    {
        DOVirtual.DelayedCall(time, () =>
        {
            _fadeImage.DOFade(0f, 1f).SetEase(Ease.InOutQuad).SetUpdate(true).OnComplete(() =>
            {
                _fade.SetActive(false);
            });
        });
    }

    private void OnDestroy()
    {
        CustomEvents.OnFade -= Fade;
    }
}

[System.Serializable]
public enum FadeType
{
    StartFade = 0,
    StartFadeSlow = 1,
    FadeOut = 2,
    FadeOutFast = 3,
    FadeOutPrologue = 4,
}
