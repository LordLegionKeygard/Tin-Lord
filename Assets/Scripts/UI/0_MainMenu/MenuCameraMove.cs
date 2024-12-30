using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMove : MonoBehaviour
{
    public List<MovePointWrapper> _movePoints; // Список точек с разными скоростями

    private int _currentPointIndex = 0; // Текущая цель
    private bool _movingToEndPoint = true; // Указывает, движемся ли к EndPoint
    private float _timeRemaining; // Время для текущей точки

    void Start()
    {
        if (_movePoints != null && _movePoints.Count > 0)
        {
            _timeRemaining = _movePoints[_currentPointIndex].Time;
        }
    }

    void Update()
    {
        if (_movePoints == null || _movePoints.Count == 0) return;

        // Получаем текущую пару точек
        MovePointWrapper currentPoint = _movePoints[_currentPointIndex];
        Transform target = _movingToEndPoint ? currentPoint.EndPoint : currentPoint.StartPoint;
        float speed = currentPoint.Speed;

        if (target == null) return; // Проверяем, есть ли точка

        // Уменьшаем оставшееся время
        _timeRemaining -= Time.deltaTime;

        // Перемещаем камеру к цели
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Применяем плавное вращение к параметрам цели
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, speed * Time.deltaTime);

        // Если достигли цели или время вышло
        if (Vector3.Distance(transform.position, target.position) < 0.1f || _timeRemaining <= 0)
        {
            if (_movingToEndPoint)
            {
                // Перемещение к EndPoint завершено, мгновенно переносим к StartPoint следующей пары
                _movingToEndPoint = false;
                _currentPointIndex = (_currentPointIndex + 1) % _movePoints.Count;
                _timeRemaining = _movePoints[_currentPointIndex].Time;
                transform.position = _movePoints[_currentPointIndex].StartPoint.position;
                transform.rotation = _movePoints[_currentPointIndex].StartPoint.rotation;
            }
            else
            {
                // Начинаем движение к EndPoint текущей пары
                _movingToEndPoint = true;
                _timeRemaining = currentPoint.Time;
            }
        }
    }
}

[System.Serializable]
public class MovePointWrapper
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float Speed;
    public float Time; // Время, отведенное на движение к точке
}
