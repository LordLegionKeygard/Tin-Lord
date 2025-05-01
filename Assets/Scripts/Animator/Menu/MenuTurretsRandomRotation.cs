using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MenuTurretsRandomRotation : MonoBehaviour
{
    [SerializeField] private int _firstAngle;
    [SerializeField] private int _secondAngle;
    [SerializeField] private int _rotationSpeed;

    private Tween _tween;
    private void Start()
    {
        StartCoroutine(RotateLoop());
    }

    private IEnumerator RotateLoop()
    {
        while (true)
        {
            float delayBefore = Random.Range(0f, 3f);
            yield return new WaitForSeconds(delayBefore);

            _tween = transform.DOLocalRotate(new Vector3(0, Random.Range(_firstAngle, _secondAngle), 0), _rotationSpeed, RotateMode.Fast).SetEase(Ease.Linear);

            // Ждем пока крутится (чтобы следующее вращение не мешало)
            yield return new WaitForSeconds(_rotationSpeed);

            // Ждем случайное время после вращения
            float delayAfter = Random.Range(1f, 3f);
            yield return new WaitForSeconds(delayAfter);
        }
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
