using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _targets;

	private int _currentTarget = 0;
	private void Update()
	{
		if (Vector2.Distance(transform.position, _targets[_currentTarget].position) < 0.01f)
		{
			_currentTarget = (1 + _currentTarget) % _targets.Length;
			transform.localScale *= new Vector2(-1, 1);
		}

		transform.position = Vector2.MoveTowards(transform.position, _targets[_currentTarget].position, _speed * Time.deltaTime);
	}
}
