using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private float _health = 100;

	public event Action<float, float> ValueChanged;

	public int MaxHealth { get; } = 100;
	public int MinHealth { get; } = 0;
	public float Value => _health;

	public void DecreaseHealth(float damage)
	{
		if (damage > 0)
		{
			_health -= damage;
			_health = Mathf.Max(_health, MinHealth);
			ValueChanged?.Invoke(_health, MaxHealth);
		}
	}

	public void IncreaseHealth(float heal)
	{
		if (heal > 0)
		{
			_health += heal;
			_health = Mathf.Min(_health, MaxHealth);
			ValueChanged?.Invoke(_health, MaxHealth);
		}
	}
}
