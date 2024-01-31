using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
	private const float Epsilon = 0.01f;

	public readonly int Speed = Animator.StringToHash(nameof(Speed));

	[SerializeField] private float _speed;
	[SerializeField] private Transform[] _targets;

	private int _currentTarget = 0;
	private bool _isFoundPlayer = false;
	private Animator _animator;
	private Player _playerTarget;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{

		else
		{
			StartCoroutine(ReachTarget());
		}

	}

	private void Catching() 
	{

	}

	private void Patrolling()
	{
		if (Vector2.Distance(transform.position, _targets[_currentTarget].position) > Epsilon)
		{
			_animator.SetFloat(Speed, 1);
			transform.position = Vector2.MoveTowards(transform.position, _targets[_currentTarget].position, _speed * Time.deltaTime);
		}
	}

	private IEnumerator ReachTarget()
	{
		_currentTarget = (++_currentTarget) % _targets.Length;
		transform.localScale *= new Vector2(-1, 1);
		yield return new WaitForSeconds(2);
	}	
}
