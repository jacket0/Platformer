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
	[SerializeField] private EnemyAttackZone _attackZone;

	private Animator _animator;
	private Coroutine _coroutine;
	private RaycastHit2D _platformBoarderHit;
	private RaycastHit2D _backVisionHit;
	private Vector3 _moveDirection = new Vector3(1, 0, 0);
	private GameObject _attackCollider;

	public event Action<int> UpdateHealth;

	public Health Health { get; private set; }


	private void Start()
	{
		_animator = GetComponent<Animator>();
		Health = GetComponent<Health>();

		_attackZone.Attacking += StartAttack;
	}

	private void OnDisable()
	{
		StopAttack();
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

	private void StopAttack()
	{
		if (_coroutine != null)
		{
			StopCoroutine(_coroutine);
		}

		_attackZone.Attacking -= StartAttack;
	}

	private void StartAttack(Player player)
	{
		_coroutine = StartCoroutine(Attacking(player));
	}

	private IEnumerator Attacking(Player player)
	{
		var wait = new WaitForSecondsRealtime(_attackReload);

		while (_attackZone.IsTargetDetected)
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
