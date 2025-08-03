using UnityEngine;

public class EnemyScale : MonoBehaviour
{
    public void SetScale(bool isMiniBoss)
    {
        Vector3 originalScale = transform.localScale;

        if (isMiniBoss)
        {
            transform.localScale = originalScale * WorldGameInfo.MiniBossScale;
        }
        else
        {
            float randomFactor = Random.Range(0.8f, 1.2f);
            transform.localScale = originalScale * randomFactor;
        }
    }
}
