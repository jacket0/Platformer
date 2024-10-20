using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Player _player;
    [SerializeField] protected float _duration = 6f;
    [SerializeField] protected float _vampirismReloadTime = 4f;

    protected Coroutine _reloadCoroutine;

    public bool IsAvailable { get; protected set; } = true;
    public float Duration => _duration;
}
