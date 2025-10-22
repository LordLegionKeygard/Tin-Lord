using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }
}
