using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class Enemy : MonoBehaviour
{
	private const float Vision = 4f;
	private const float Epsilon = 0.5f;

	public readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	[SerializeField] private float _speed;
	[SerializeField] private int _damage = 25;
	[SerializeField] private float _attackReload = 1f;
	[SerializeField] private LayerMask _playerMask;
	[SerializeField] private EnemyAttackZone _attackZone;
	[SerializeField] private Transform[] _patrolTargets;
	[SerializeField] private Transform _playerTarget;

	private Animator _animator;
	private Coroutine _coroutine;
	private RaycastHit2D _backVisionHit;
	private RaycastHit2D _forwardVisinHit;
	private bool _IsChase = false;
	private Quaternion _rightDirection = Quaternion.Euler(0, 0, 0);
	private Quaternion _leftDirection = Quaternion.Euler(0, 180, 0);
	private int _currentIndex;

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
		_backVisionHit = Physics2D.Raycast(transform.position, Vector2.left, Vision, _playerMask);
		Debug.DrawRay(transform.position, Vector2.left * Vision, Color.red);
		_forwardVisinHit = Physics2D.Raycast(transform.position, Vector2.right, Vision, _playerMask);


		if (_backVisionHit || _forwardVisinHit)
		{
			_IsChase = true;
		}
		else
		{
			_IsChase = false;
		}
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		Vector2 currentTarget;

		if (_IsChase)
		{
			currentTarget = _playerTarget.position;
		}
		else
		{
			currentTarget = _patrolTargets[_currentIndex].position;
		}

		transform.position = Vector2.MoveTowards(transform.position, currentTarget, _speed * Time.deltaTime);

		if (Vector2.Distance(transform.position, _patrolTargets[_currentIndex].position) < Epsilon)
		{
			_currentIndex = (++_currentIndex) % _patrolTargets.Length;

		}

		if (transform.position.x < currentTarget.x)
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		if (transform.position.x > currentTarget.x)
		{
			transform.rotation = Quaternion.Euler(0, 180, 0);
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
}
