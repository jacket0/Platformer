using System;
using UnityEngine;

public class Gem : Item
{
	[SerializeField] private int _achievementPoints = 1;

	public override event Action<Vector2> ItemPicked;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			Destroy(gameObject);
			ItemPicked?.Invoke(gameObject.transform.position);
		}
	}
}
