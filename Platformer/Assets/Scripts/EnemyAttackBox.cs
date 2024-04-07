using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackBox : MonoBehaviour
{
	[SerializeField] private UnityEvent PlayerDetected;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			PlayerDetected.Invoke();
		}
	}
}
