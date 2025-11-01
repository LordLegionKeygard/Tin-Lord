using UnityEngine;

public class WaterVFXTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WaterVFX waterVFX))
        {
            waterVFX.ParticleToggle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WaterVFX waterVFX))
        {
            waterVFX.ParticleToggle(false);
        }
    }
}
