using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Healing : Item 
{
	[SerializeField] private int _healingPoints = 40;

	private Player _player;

	public override event Action<Vector2> ItemPicked;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out _player))
		{
			Destroy(gameObject);
			IncreaseHealthPoints(gameObject.transform.position);
			ItemPicked.Invoke(gameObject.transform.position);
		}
	}

	private void IncreaseHealthPoints(Vector2 position)
    {
        _player.Health.IncreaseHealth(_healingPoints);
    }
}
