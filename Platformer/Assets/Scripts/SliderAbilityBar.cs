using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderAbilityBar : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Slider _slider;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _vampirism.LifeStealing += IncreaseReloadBar;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _vampirism.LifeStealing -= IncreaseReloadBar;
    }

    private void IncreaseReloadBar(float time, bool isActive)
    {
        _coroutine = StartCoroutine(Increase(time, isActive));
    }

    private IEnumerator Increase(float time, bool isActive)
    {
        float goal = isActive ? _slider.maxValue : _slider.minValue;

        while (_slider.value != goal)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, goal, _slider.maxValue / time * Time.deltaTime);
            yield return null;
        }
    }
}
