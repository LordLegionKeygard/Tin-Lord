using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float _rotationSpeed = 2;

    public void MoveTo(Vector3 targetPosition, float speed)
    {
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
