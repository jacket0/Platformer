using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyVision : MonoBehaviour
{
	[SerializeField] private UnityEvent<Transform> PlayerDetected;
	[SerializeField] private UnityEvent PlayerLost;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			PlayerDetected.Invoke(collision.transform);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			PlayerLost.Invoke();
		}
	}
}
