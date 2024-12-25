using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLoadingScreenSlider : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Image _sliderImage;

    private void OnEnable()
    {
        _sliderImage.fillAmount = 0;
    }

    public void Update()
    {
        _sliderImage.fillAmount = Mathf.MoveTowards(_sliderImage.fillAmount, _sceneLoader.LoadingProgress, 0.5f * Time.deltaTime);
    }
}
