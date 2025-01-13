using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private float _value = 100;

	public event Action<float, float> ValueChanged;

	public int MaxValue { get; } = 100;
	public int MinValue { get; } = 0;

	public void DecreaseValue(float damage)
	{
		if (damage > 0)
		{
			_value -= damage;
			_value = Mathf.Max(_value, MinValue);
			ValueChanged?.Invoke(_value, MaxValue);
		}
	}

	public void IncreaseValue(float heal)
	{
		if (heal > 0)
		{
			_value += heal;
			_value = Mathf.Min(_value, MaxValue);
			ValueChanged?.Invoke(_value, MaxValue);
		}
	}
}
