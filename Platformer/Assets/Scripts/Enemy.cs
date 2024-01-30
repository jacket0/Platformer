using UnityEngine;

public class Enemy : MonoBehaviour
{
	private const float Epsilon = 0.01f;

	[SerializeField] private float _speed;
	[SerializeField] private Transform[] _targets;

	private int _currentTarget = 0;

	private void Update()
	{
		if (Vector2.Distance(transform.position, _targets[_currentTarget].position) < Epsilon)
		{
			_currentTarget = (++_currentTarget) % _targets.Length;
			transform.localScale *= new Vector2(-1, 1);
		}

		transform.position = Vector2.MoveTowards(transform.position, _targets[_currentTarget].position, _speed * Time.deltaTime);
	}
}
