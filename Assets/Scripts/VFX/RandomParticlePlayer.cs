using UnityEngine;
using System.Collections;

public class RandomParticlePlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;
    [SerializeField] private Vector2 _firstDelayRange;
    [SerializeField] private Vector2 _loopDelayRange;


    private void OnEnable()
    {
        StartCoroutine(PlayLoop());
    }

    private IEnumerator PlayLoop()
    {
        yield return new WaitForSeconds(Random.Range(_firstDelayRange.x, _firstDelayRange.y));

        while (isActiveAndEnabled)
        {
            PlayRandom();

            yield return new WaitForSeconds(Random.Range(_loopDelayRange.x, _loopDelayRange.y));
        }
    }

    private void PlayRandom()
    {
        var rnd = Random.Range(0, _particleSystems.Length);
        _particleSystems[rnd].Play();       
    }
}
