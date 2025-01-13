using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite.enabled = false;
    }

    public float GetSize()
    {
        return _sprite.transform.position.x;
    }

    public void Show()
    {
        _sprite.enabled = true;
    }

    public void Hide()
    {
        _sprite.enabled = false;
    }
}
