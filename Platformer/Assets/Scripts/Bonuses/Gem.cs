using System;
using UnityEngine;

public class Gem : Item
{
	public void CollectGemBonus()
	{
		Destroy(gameObject);

	}
}
