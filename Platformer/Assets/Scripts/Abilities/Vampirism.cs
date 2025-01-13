using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Vampirism : Ability
{
    [SerializeField] private float _stealingPower = 0.1f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Sprite _sprite;

    private Coroutine _reloadCoroutine;
    private Coroutine _activeCoroutine;
    private WaitForSeconds _relaodTime;
    private float _radius;
    private PlayerInput _playerInput;

    public event Action<float, bool> LifeStealing;

    private void Awake()
    {
        _relaodTime = new WaitForSeconds(ReloadTime);
        _radius = _sprite.GetSize();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnDisable()
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        if (_reloadCoroutine != null)
            StopCoroutine(_reloadCoroutine);

        _playerInput.VampirismActivated -= SwitchActive;
    }

    private void OnEnable()
    {
        _playerInput.VampirismActivated += SwitchActive;
    }

    private IEnumerator HitPointStealing()
    {
        _sprite.Show();
        float wait = 0;
        LifeStealing?.Invoke(Duration, IsAvailable);

        while (wait != Duration)
        {
            var enemy = FindEnemy();

            if (enemy != null)
            {
                enemy.Health.DecreaseValue(_stealingPower);
                Player.Health.IncreaseValue(_stealingPower);
            }

            wait = Mathf.MoveTowards(wait, Duration, Time.deltaTime);
            yield return null;
        }

        _sprite.Hide();
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

