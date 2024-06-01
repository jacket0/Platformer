using System;
using UnityEngine;

public class Healing : Item
{
	[SerializeField] private int _healingPoints = 40;

	private Player _player;

	public override event Action<Vector2> ItemPicked;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out _player))
		{
			ItemPicked.Invoke(gameObject.transform.position);
			Destroy(gameObject);
			IncreaseHealthPoints();
		}
	}

	private void IncreaseHealthPoints()
	{
		Debug.Log(_player.Health);

		_player.Health.IncreaseHealth(_healingPoints);
	}
}
