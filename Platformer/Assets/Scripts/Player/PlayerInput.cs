using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _vamprirsmKey;
    [SerializeField] private KeyCode _attackKey;

    public event Action VampirismActivated;
    public event Action Attacked;

    private void Update()
    {
        if (Input.GetKeyDown(_vamprirsmKey))
        {
            VampirismActivated?.Invoke();
        }

        if (Input.GetKeyDown(_attackKey))
        {
            Attacked?.Invoke();
        }
    }

}
