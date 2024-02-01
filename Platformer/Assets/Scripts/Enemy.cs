using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
	private const float Epsilon = 0.01f;
	private const float RayDistance = 2f;
	private const float Negative = -1f;

	[SerializeField] private float _speed;
	[SerializeField] private int _health = 100;
	[SerializeField] private int _damage = 25;
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private Player _player;

	private int _currentTargetIndex = 0;
	private Animator _animator;
	private RaycastHit2D _attackHit;
	private Coroutine _coroutine;
	private RaycastHit2D _platformBoarder;
	private Vector3 _moveDirection = new Vector3(1, 0, 0);

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		_attackHit = Physics2D.Raycast(transform.position, Vector2.left * _moveDirection, RayDistance);
		Debug.DrawRay(transform.position, Vector2.right * RayDistance, Color.red);

		_platformBoarder = Physics2D.Raycast(transform.position + _moveDirection, Vector2.down, RayDistance, _groundMask);
		Debug.DrawRay(transform.position + _moveDirection, Vector2.down * RayDistance, Color.blue);

		_coroutine = StartCoroutine(Stalking());
		ChangeDirection();
	}

	private void ChangeDirection()
	{
		if (!_platformBoarder)
		{
			transform.localScale *= new Vector2(-1, 1);
			_moveDirection *= Negative;

		}
	}

	public void Catching() 
	{
		_coroutine = StartCoroutine(Follow());
	}

	private IEnumerator Follow()
	{
		while (Vector2.Distance(transform.position, _player.transform.position) > Epsilon)
		{
			transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, Time.deltaTime);

			yield return null;
		}
	}

	public void Patrolling()
	{
		StopCoroutine(_coroutine);

		_coroutine = StartCoroutine(Stalking());
	}

	private IEnumerator Stalking()
	{
		transform.Translate(_moveDirection * Time.deltaTime * _speed);
		yield return null;
	}
}
