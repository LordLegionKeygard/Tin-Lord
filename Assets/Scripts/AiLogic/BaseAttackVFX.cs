using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;

    public void PlayVFX(int number)
    {
        _particleSystems[number].Play();
    }
}
