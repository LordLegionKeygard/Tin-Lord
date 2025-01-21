using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RandomSlidersAnimation : MonoBehaviour
{
    [SerializeField] private List<Slider> _sliders;
    private float _interval = 3;
    private float _duration = 2;
    
    private void Start()
    {
        StartCoroutine(SpawnAnimations());
    }


    private IEnumerator SpawnAnimations()
    {
        while (true)
        {
            int index = Random.Range(0, _sliders.Count);
            float targetValue = Random.Range(0f, 1f);
            
            StartCoroutine(AnimateSlider(_sliders[index], targetValue, _duration));
            
            yield return new WaitForSeconds(_interval);
        }
    }


    private IEnumerator AnimateSlider(Slider slider, float targetValue, float duration)
    {
        float startValue = slider.value;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            slider.value = Mathf.Lerp(startValue, targetValue, t);
            yield return null; 
        }

        slider.value = targetValue;
    }
}
