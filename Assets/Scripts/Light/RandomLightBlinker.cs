using UnityEngine;
using System.Collections;

public class RandomLightBlinker : MonoBehaviour
{
    [SerializeField] private LightRendererPair[] _pairs;
    [SerializeField] private Vector2 _firstDelayRange;
    [SerializeField] private Vector2 _loopDelayRange;
    [SerializeField] private float _blinkDuration = 0.1f;
    private string _emissionProperty = "_EmissiveIntensity";

    private void Awake()
    {
        for (int i = 0; i < _pairs.Length; i++)
        {
            var p = _pairs[i];
            if (p.MeshRenderer != null)
            {
                p.InstancedMat    = p.MeshRenderer.material;
                p.DefaultEmission = p.InstancedMat.HasProperty(_emissionProperty)
                    ? p.InstancedMat.GetFloat(_emissionProperty)
                    : 0.8f;
                _pairs[i] = p;
            }
        }
    }

    private void OnEnable() => StartCoroutine(BlinkLoop());

    private IEnumerator BlinkLoop()
    {
        yield return new WaitForSeconds(Random.Range(_firstDelayRange.x, _firstDelayRange.y));

        while (isActiveAndEnabled)
        {
            if (_pairs == null || _pairs.Length == 0) yield break;

            int idx  = Random.Range(0, _pairs.Length);
            var pair = _pairs[idx];

            SetState(pair, false);
            yield return new WaitForSeconds(_blinkDuration);
            SetState(pair, true);

            yield return new WaitForSeconds(Random.Range(_loopDelayRange.x, _loopDelayRange.y));
        }
    }

    private void SetState(LightRendererPair pair, bool enable)
    {
        if (pair.PointLight != null) pair.PointLight.enabled = enable;
        if (pair.InstancedMat != null && pair.InstancedMat.HasProperty(_emissionProperty))
            pair.InstancedMat.SetFloat(_emissionProperty, enable ? pair.DefaultEmission : 0f);
    }
}

[System.Serializable]
public struct LightRendererPair
{
    public MeshRenderer MeshRenderer;
    public Light PointLight;
    [HideInInspector] public Material InstancedMat;
    [HideInInspector] public float DefaultEmission;
}
