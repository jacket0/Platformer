using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int _health = 100;

	public void DecreaseHealth(int damage)
	{
		if (_health > 0)
		{
			_health -= damage;
			Debug.Log("Здоровье: " + _health);
		}
	}

	public void IncreaseHealth(int heal)
	{
		if (_health < 100)
		{
			_health += heal;
		}

		if (_health + heal > 100)
		{
			_health = 100;
		}

		Debug.Log("Излеченной здоровье: " + _health);
	}
}
