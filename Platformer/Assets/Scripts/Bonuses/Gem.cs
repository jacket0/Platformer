using System;
using UnityEngine;

public class Gem : Item
{
	public void ExecuteGemBonus()
	{
		Destroy(gameObject);

	}
}
