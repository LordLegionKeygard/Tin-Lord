using UnityEngine;

public class WaterSlowTrigger : MonoBehaviour
{
    private float _slowAmount = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        var enemySpeed = other.GetComponent<EnemyDebuff>();
        if (enemySpeed != null)
        {
            enemySpeed.ChangeSlowDebuff(-_slowAmount, SlowType.River);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemySpeed = other.GetComponent<EnemyDebuff>();
        if (enemySpeed != null)
        {
            enemySpeed.ChangeSlowDebuff(+_slowAmount, SlowType.River);
        }
    }
}
