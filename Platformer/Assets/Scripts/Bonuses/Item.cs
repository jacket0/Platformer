using System;
using UnityEngine;

public class Item : MonoBehaviour
{
	public event Action<Vector2> ItemPicked;

	public void InvokePickedEvent()
	{
		ItemPicked?.Invoke(gameObject.transform.position);
	}
}
