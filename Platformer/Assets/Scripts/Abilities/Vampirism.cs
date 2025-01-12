using System;
using System.Collections;
using UnityEngine;

public class Vampirism : Ability
{
    [SerializeField] private float _stealingPower = 0.1f;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private KeyCode _vamprirsmKey;
    [SerializeField] private LayerMask _enemyLayer;

    private Coroutine _reloadCoroutine;
    private Coroutine _activeCoroutine;
    private WaitForSeconds _relaodTime;
    private float _radius;

    public event Action<float, bool> LifeStealing;

    private void Awake()
    {
        _sprite.enabled = false;
        _relaodTime = new WaitForSeconds(ReloadTime);
        _radius = _sprite.transform.localScale.x * 0.5f;
    }

    private void OnDisable()
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        if (_reloadCoroutine != null)
            StopCoroutine(_reloadCoroutine);
    }

    private void Update()
    {
        VampirismOn();
    }

    private void VampirismOn()
    {
        if (Input.GetKeyDown(_vamprirsmKey))
            SwitchActive();
    }

    private IEnumerator HitPointStealing()
    {
        _sprite.enabled = true;
        float wait = 0;
        LifeStealing?.Invoke(Duration, IsAvailable);

        while (wait != Duration)
        {
            var enemy = FindEnemy();

            if (enemy != null)
            {
                enemy.Health.DecreaseHealth(_stealingPower);
                Player.Health.IncreaseHealth(_stealingPower);
            }

            wait = Mathf.MoveTowards(wait, Duration, Time.deltaTime);
            yield return null;
        }

        _sprite.enabled = false;
        _reloadCoroutine = StartCoroutine(AbilityReloading());
    }

    private IEnumerator AbilityReloading()
    {
        LifeStealing?.Invoke(ReloadTime, !IsAvailable);
        yield return _relaodTime;
        IsAvailable = true;
    }

    private Enemy FindEnemy()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _radius, _enemyLayer);

        if (collider == null)
            return null;

        collider.TryGetComponent(out Enemy enemy);
        return enemy;
    }

    protected override void SwitchActive()
    {
        if (IsAvailable)
        {
            IsAvailable = false;
            _activeCoroutine = StartCoroutine(HitPointStealing());
        }
    }
}

