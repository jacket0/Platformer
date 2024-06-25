using System;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
	private event Action PlayerDetected;

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	if (collision.GetComponent<Player>() != null)
	//	{
	//		PlayerDetected?.Invoke();
	//	}
	//}
}
