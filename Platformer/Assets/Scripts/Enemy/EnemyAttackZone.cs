using System;
using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
	[SerializeField] private int _damage = 20;

	public event Action<Player> Attacking;

	public bool IsTargetDetected { get; private set; } = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Player player))
		{
			IsTargetDetected = true;
			Attacking?.Invoke(player);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Player player))
		{
			IsTargetDetected = false;
		}
	}
}
