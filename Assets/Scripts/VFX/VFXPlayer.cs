using UnityEngine;

public class VFXPlayer: MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystem;

    public void PlayVFX(int number)
    {
        _particleSystem[number].Play();
    }
}
