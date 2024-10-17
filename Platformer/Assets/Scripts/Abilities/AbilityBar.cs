using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImeSlider : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _fillingSpeed;

    private Coroutine _coroutine;

    protected void UpdateTime(float currentValue, float maxValue)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothFill(currentValue, maxValue));
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SmoothFill(float currentValue, float maxValue)
    {
        float goal = currentValue / maxValue;

        while (_slider.value != goal)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, goal, _fillingSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
