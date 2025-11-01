using UnityEngine;

public class WaterVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _waterParticle;

    public void ParticleToggle(bool state)
    {
        foreach (var item in _waterParticle)
        {
            if (state) item.Play();
            else item.Stop();
        }
    }
}
