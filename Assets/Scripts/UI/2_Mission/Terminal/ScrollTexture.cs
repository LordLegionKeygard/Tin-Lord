using UnityEngine;
using UnityEngine.UI;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private RawImage _rawImage;


    private void Update()
    {
        if (_rawImage != null)
        {
            Vector2 offset = _rawImage.uvRect.position;
            offset.x += _scrollSpeed * Time.deltaTime;

            _rawImage.uvRect = new Rect(offset, _rawImage.uvRect.size);
        }
    }
}
