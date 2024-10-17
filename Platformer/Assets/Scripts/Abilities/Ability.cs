using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected AbilitySwitcher _switcher;
    [SerializeField] protected float _duration = 6f;
}
