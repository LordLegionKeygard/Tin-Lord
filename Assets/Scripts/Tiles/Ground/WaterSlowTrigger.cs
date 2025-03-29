using UnityEngine;

public class WaterSlowTrigger : MonoBehaviour
{
    private float _slowAmount = 0.7f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyDebuff>(out var enemySpeed))
        {
            enemySpeed.ChangeSlowDebuff(-_slowAmount, SlowType.River);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<EnemyDebuff>(out var enemySpeed))
        {
            enemySpeed.ChangeSlowDebuff(+_slowAmount, SlowType.River);
        }
    }
}
