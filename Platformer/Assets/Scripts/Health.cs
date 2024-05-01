using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int _health = 100;

	private readonly int _maxHealth = 100;
	private readonly int _minHealth = 0;

	public void DecreaseHealth(int damage)
	{
		if (_health > _minHealth)
		{
			_health -= damage;
			Debug.Log("Здоровье: " + _health);
		}
	}

	public void IncreaseHealth(int heal)
	{
		_health += heal;
		Mathf.Clamp(_health, _minHealth, _maxHealth);
		Debug.Log("Излеченной здоровье: " + _health);
	}
}
