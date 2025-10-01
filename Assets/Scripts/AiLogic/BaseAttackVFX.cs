using UnityEngine;

public class BaseAttackVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;

    public void PlayAttackVFX(int number)
    {
        _particleSystems[number].Play();
    }
}
