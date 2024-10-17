using System;
using System.Collections;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] private KeyCode _vamprirsmKey;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] protected float _reloadTime = 8f;

    private Coroutine _reloadCoroutine;

    public event Action VampirismActivated;

    public bool IsAvailable { get; protected set; } = true;

    private void OnEnable()
    {
        _vampirism.Reloading += StartReloading;
    }

    private void OnDisable()
    {
        if(_reloadCoroutine != null)
            StopCoroutine(_reloadCoroutine);

        _vampirism.Reloading -= StartReloading;
    }

    private void Update()
    {
        if (IsAvailable && Input.GetKeyDown(_vamprirsmKey))
        {
            _vampirism.gameObject.SetActive(IsAvailable);
            IsAvailable = false;
            VampirismActivated?.Invoke();
        }
    }

    private void StartReloading()
    {
        _reloadCoroutine = StartCoroutine(AbilityReloading());
    }

    private IEnumerator AbilityReloading()
    {
        yield return new WaitForSeconds(_reloadTime);
        IsAvailable = true;
    }
}
