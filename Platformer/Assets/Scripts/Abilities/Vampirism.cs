using System;
using System.Collections;
using UnityEngine;

public class Vampirism : Ability
{
    [SerializeField] private float _stealingPower = 0.1f;

    private Coroutine _coroutine;

    public event Action Reloading;

    private void OnEnable()
    {
        _switcher.VampirismActivated += SwitchActive;
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _switcher.VampirismActivated -= SwitchActive;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.activeSelf && collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.DecreaseHealth(_stealingPower);
        }
    }

    protected IEnumerator HealthStealing()
    {
        yield return new WaitForSeconds(_duration);
        Reloading.Invoke();
        gameObject.SetActive(false);
    }

    private void SwitchActive()
    {
        _coroutine = StartCoroutine(HealthStealing());
    }
}

