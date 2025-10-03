using System.Collections;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] private GameObject _activeObject;
    [SerializeField] private GameObject[] _disableObjects;
    [SerializeField] private Rigidbody[] _rigidbodies;

    [Header("Random impulse settings")]
    [SerializeField] private bool _needRandomImpulse;
    [SerializeField] private float _minImpulse;     // минимальный импульс на тело
    [SerializeField] private float _maxImpulse;     // максимальный импульс на тело
    [SerializeField, Range(0f, 1f)] private float upBias; // «тянем» направление чуть вверх
    [SerializeField] private bool _addSpin;       // добавить крутящий момент
    [SerializeField] private float _maxTorque;      // максимум для случайного torque

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActiveRagdoll()
    {
        if (_activeObject != null)
        {
            foreach (var item in _disableObjects)
            {
                item.SetActive(false);
            }
            _activeObject.SetActive(true);
        }
        else
        {
            _animator.enabled = false;
            KinematicToggle(false);
            StartCoroutine(nameof(ReturnKinematic));
        }

        if (_needRandomImpulse) RandomImpulse();
    }

    private IEnumerator ReturnKinematic()
    {
        yield return new WaitForSeconds(2f);
        KinematicToggle(true);
    }


    public void KinematicToggle(bool state)
    {
        foreach (var rb in _rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = state;
        }
    }

    private void RandomImpulse()
    {
        foreach (var rb in _rigidbodies)
        {
            if (rb == null || rb.isKinematic) continue;

            // Случайное направление с небольшим уклоном вверх
            Vector3 dir = (Random.onUnitSphere + Vector3.up * upBias).normalized;

            float impulse = Random.Range(_minImpulse, _maxImpulse);
            rb.AddForce(dir * impulse, ForceMode.Impulse);

            if (_addSpin)
            {
                // Случайный крутящий момент для более «живого» разлёта
                Vector3 torque = new Vector3(
                    Random.Range(-_maxTorque, _maxTorque),
                    Random.Range(-_maxTorque, _maxTorque),
                    Random.Range(-_maxTorque, _maxTorque)
                );
                rb.AddTorque(torque, ForceMode.Impulse);
            }
        }
    }
}
