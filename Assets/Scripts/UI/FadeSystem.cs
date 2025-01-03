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
        }
    }

    private void StartFade(float time)
    {
        _fade.SetActive(true);  // Включаем объект сразу
        DOVirtual.DelayedCall(time, () =>
        {
            _fadeImage.DOFade(1f, 0.15f).SetEase(Ease.InOutQuad).SetUpdate(true);  // Затемняем за 0.5 сек до полного значения 1
        });
    }

    private void FadeOut(float time)
    {
        DOVirtual.DelayedCall(time, () =>
        {
            _fadeImage.DOFade(0f, 1f).SetEase(Ease.InOutQuad).SetUpdate(true).OnComplete(() =>
            {
                _fade.SetActive(false);  // После исчезновения делаем объект неактивным
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
}
