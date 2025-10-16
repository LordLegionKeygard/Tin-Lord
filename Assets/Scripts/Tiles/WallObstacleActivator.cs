using UnityEngine;

public class WallObstacleActivator : MonoBehaviour
{
    [SerializeField] private GameObject obstacleA;
    [SerializeField] private GameObject obstacleB;

    private void OnEnable()
    {
        // Базу уничтожили, но после этого достроилась стена
        if (BasePoint.Instance == null)
        {
            return;
        }

        Vector3 basePosition = BasePoint.Instance.transform.position;

        float distA = Vector3.Distance(obstacleA.transform.position, basePosition);
        float distB = Vector3.Distance(obstacleB.transform.position, basePosition);

        if (distA < distB)
        {
            EnableObstacle(obstacleA);
            DisableObstacle(obstacleB);
        }
        else
        {
            EnableObstacle(obstacleB);
            DisableObstacle(obstacleA);
        }
    }

    private void EnableObstacle(GameObject obstacle)
    {
        obstacle.SetActive(true);
    }

    private void DisableObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
    }

    private void OnDisable()
    {
        DisableObstacle(obstacleA);
        DisableObstacle(obstacleB);
    }
}
