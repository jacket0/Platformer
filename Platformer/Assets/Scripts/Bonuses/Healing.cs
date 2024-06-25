using UnityEngine;

public class Healing : Item
{
	[SerializeField] private int _healingPoints = 40;

	public int ExecuteHealing()
	{
		Destroy(gameObject);
		return _healingPoints;
	}
}
