using UnityEngine;

public class ObjectToPointsMove : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed; 
    [SerializeField] private float _rotationSpeed;
    private int _currentPointIndex = 0;

    private void Update()
    {
        if (_points.Length == 0)
            return;

        
        Transform targetPoint = _points[_currentPointIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);

        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
        }
    }
}