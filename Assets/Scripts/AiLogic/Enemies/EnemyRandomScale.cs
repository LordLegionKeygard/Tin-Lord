using UnityEngine;

public class EnemyRandomScale : MonoBehaviour
{
    private void Start()
    {
        Vector3 originalScale = transform.localScale;
        
        float randomFactor = Random.Range(0.8f, 1.2f);

        transform.localScale = originalScale * randomFactor;
    }
}
