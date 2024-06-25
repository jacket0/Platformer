using System;
using UnityEngine;

public class Gem : Item
{
	[SerializeField] private int _achievementPoints = 1;

	public void ExecuteGemBonus()
	{
		Destroy(gameObject);

	}
}
