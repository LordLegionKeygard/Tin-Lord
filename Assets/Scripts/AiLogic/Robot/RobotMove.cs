using UnityEngine;

public class RobotMove : MonoBehaviour
{
    private float _rotationSpeed = 2;
    private RobotSpeed _robotSpeed;

    private void Awake()
    {
        _robotSpeed = GetComponent<RobotSpeed>();
    }

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
}
