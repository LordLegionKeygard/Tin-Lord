using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class EnableAlphaHit : MonoBehaviour
{
    private void Awake()
    {
        var img = GetComponent<Image>();
        img.alphaHitTestMinimumThreshold = 0.1f;
    }
}
