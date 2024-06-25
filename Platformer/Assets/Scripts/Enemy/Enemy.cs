using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class Enemy : MonoBehaviour
{
	private const float PlatformHeightDistance = 2f;
	private const float Vision = 4f;
	private const float NegativeConst = -1f;

	public readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	[SerializeField] private float _speed;
	[SerializeField] private int _damage = 25;
	[SerializeField] private float _attackReload = 1f;
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private LayerMask _playerMask;
	private Health _health;

	private Animator _animator;
	private RaycastHit2D _platformBoarderHit;
	private RaycastHit2D _backVisionHit;
	private Vector3 _moveDirection = new Vector3(1, 0, 0);

	public event Action<int> UpdateHealth;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_health = GetComponent<Health>();
	}

	private void Update()
	{
		_platformBoarderHit = Physics2D.Raycast(transform.position + _moveDirection / 2, Vector2.down, PlatformHeightDistance, _groundMask);
		Debug.DrawRay(transform.position + _moveDirection / 2, Vector2.down * PlatformHeightDistance, Color.blue);

		_backVisionHit = Physics2D.Raycast(transform.position, -_moveDirection, Vision, _playerMask);
		Debug.DrawRay(transform.position, -_moveDirection * Vision, Color.black);

		transform.Translate(_platformBoarderHit.centroid.normalized * Time.deltaTime * _speed);

		if (_backVisionHit)
		{
			ChangeDirection();
		}

		if (!_platformBoarderHit)
		{
			ChangeDirection();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Player player))
		{
			StartCoroutine(Attacking(player));
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		StopAllCoroutines();
	}

	private IEnumerator Attacking(Player player)
	{
		var wait = new WaitForSecondsRealtime(_attackReload);

		while (true)
		{
			_animator.SetTrigger(DoAttack);
			player.Health.DecreaseHealth(_damage);
			yield return wait;
		}
	}

	private void ChangeDirection()
	{
		transform.Rotate(new Vector3(0, 180, 0));
		_moveDirection *= NegativeConst;
	}
}