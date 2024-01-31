using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
	public event Action<Vector2> GemDestroed;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			Destroy(gameObject);
			GemDestroed?.Invoke(gameObject.transform.position);
		}
	}
}
