using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int _health = 100;

	private readonly int _maxHealth = 100;
	private readonly int _minHealth = 0;

	public void DecreaseHealth(int damage)
	{
		_health -= damage;
		_health = Mathf.Clamp(_health, _minHealth, _maxHealth);
	}

	public void IncreaseHealth(int heal)
	{
		_health += heal;
		_health = Mathf.Clamp(_health, _minHealth, _maxHealth);
	}
}
