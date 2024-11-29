using UnityEngine;

public class RobotMove : MonoBehaviour
{
    private float _rotationSpeed = 2;
    private RobotSpeed _robotSpeed;

    private void Awake()
    {
        _robotSpeed = GetComponent<RobotSpeed>();
    }

    /// <summary>
    /// Для передвижения и вращения при патрулировании
    /// </summary>
    public void MoveTo(Vector3 targetPosition)
    {
        float speed = _robotSpeed.Speed();

        if (speed <= 0) return;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// Метод для поворота робота-инженера к зданию для починки
    /// </summary>
    public void RotateTo(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}
