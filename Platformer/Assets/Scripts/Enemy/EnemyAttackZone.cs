using System;
using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
	public event Action<Player> Attacking;

	public bool IsTargetDetected { get; private set; } = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("!@#");
		if (collision.TryGetComponent(out Player player))
		{
			IsTargetDetected = true;
			Attacking?.Invoke(player);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out _))
		{
			IsTargetDetected = false;
		}
	}
}
