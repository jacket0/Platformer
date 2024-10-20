using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Vampirism : Ability
{
    [SerializeField] private float _stealingPower = 0.1f;
    [SerializeField] private SpriteRenderer _sprite;

    private Coroutine _coroutine;

    public event Action<float, bool> LifeStealing;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _player.VampirismActivated += SwitchActive;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (_reloadCoroutine != null)
            StopCoroutine(_reloadCoroutine);

        _player.VampirismActivated -= SwitchActive;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.activeSelf && collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.DecreaseHealth(_stealingPower);
        }
    }

    private IEnumerator HitPointStealing()
    {
        _sprite.enabled = !_sprite.enabled;
        LifeStealing?.Invoke(_duration, IsAvailable);   
        yield return new WaitForSeconds(_duration);
        _reloadCoroutine = StartCoroutine(AbilityReloading());
    }

    private IEnumerator AbilityReloading()
    {
        _sprite.enabled = !_sprite.enabled;
        LifeStealing?.Invoke(_vampirismReloadTime, !IsAvailable);
        yield return new WaitForSeconds(_vampirismReloadTime);
        IsAvailable = true;
    }

    private void SwitchActive()
    {
        IsAvailable = false;
        _coroutine = StartCoroutine(HitPointStealing());
    }
}

