using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSniperExplosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseHealth baseHealth))
        {
            baseHealth.TakeDamage(RobotsData.Instance.GetCurrentRangeDamage() * 5, 100);
        }
    }
}
