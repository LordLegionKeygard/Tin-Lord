using DG.Tweening;
using UnityEngine;

public class LoadingGearRotation : MonoBehaviour
{
    [SerializeField] private float _startRotationZ = 0f; // Начальный угол Z
    [SerializeField] private bool _rotateClockwise = true; // Вращение по часовой стрелке (true) или против (false)
    private float _duration = 7f; // Длительность полного оборота

    private void Start()
    {
        // Устанавливаем начальный угол вращения Z
        transform.rotation = Quaternion.Euler(0f, 0f, _startRotationZ);

        // Запускаем анимацию вращения
        RotateContinuously();
    }

    private void RotateContinuously()
    {
        // Определяем направление вращения (1 — по часовой, -1 — против часовой)
        float direction = _rotateClockwise ? 1f : -1f;

        // Вращаем объект бесконечно вокруг оси Z
        transform.DORotate(new Vector3(0f, 0f, 360f * direction + _startRotationZ), _duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear) // Равномерное вращение
            .SetLoops(-1, LoopType.Incremental); // Бесконечный цикл
    }
}
