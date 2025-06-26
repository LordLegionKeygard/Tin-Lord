using UnityEngine;

public class EnemyScale : MonoBehaviour
{
    private float _miniBossScale = 1.5f;
    public void SetScale(int healthFactor, int damageFactor)
    {
        Vector3 originalScale = transform.localScale;
        if (healthFactor > 1 || damageFactor > 1)
        {
            transform.localScale = originalScale * _miniBossScale;
        }
        else
        {
            float randomFactor = Random.Range(0.8f, 1.2f);
            transform.localScale = originalScale * randomFactor;
        }
    }
}
