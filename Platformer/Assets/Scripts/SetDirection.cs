using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirection : MonoBehaviour
{
	public void ChangeDirection(SpriteRenderer _spriteRenderer, float direction)
	{
		if (direction > 0)
		{
			_spriteRenderer.flipX = false;

		}
		else if (direction < 0)
		{
			_spriteRenderer.flipX = true;
		}
	}
}
