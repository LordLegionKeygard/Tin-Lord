using System.Collections;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    [SerializeField] private GameObject _fade;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        CustomEvents.OnFade += Fade;
    }
    public void Fade(FadeType fadeType)
    {

        switch (fadeType)
        {
            case FadeType.StartFade:
                _fade.SetActive(true);
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
        StartCoroutine(ExecuteAfterTime(time));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            _fade.SetActive(true);
        }
    }

    private void FadeOut(float time)
    {
        StartCoroutine(ExecuteAfterTime(time));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            _animator.SetTrigger(AnimatorStrings.FadeOut);
            yield return new WaitForSeconds(timeInSec);
            _fade.SetActive(false);
        }
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
