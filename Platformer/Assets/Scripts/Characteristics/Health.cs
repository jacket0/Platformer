using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int _health = 100;

	public event Action<int, int> ValueChanged;

	public int MaxHealth { get; } = 100;
	public int MinHealth { get; } = 0;
	public int Value => _health;

	public void DecreaseHealth(int damage)
	{
		if (damage > 0)
		{
			_health -= damage;
			_health = Mathf.Max(_health, MinHealth);
			ValueChanged?.Invoke(_health, MaxHealth);
		}
	}

	public void IncreaseHealth(int heal)
	{
		if (heal > 0)
		{
			_health += heal;
			_health = Mathf.Min(_health, MaxHealth);
			ValueChanged?.Invoke(_health, MaxHealth);
		}
	}
}
