using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
	private const float DetectionDistance = 4f;
	private const float Epsilon = 0.5f;

	public readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	[SerializeField] private float _speed;
	[SerializeField] private int _damage = 25;
	[SerializeField] private float _attackReload = 1f;
	[SerializeField] private LayerMask _playerMask;
	[SerializeField] private EnemyAttackZone _attackZone;
	[SerializeField] private Transform[] _patrolTargets;
	[SerializeField] private Transform _playerTarget;
	[SerializeField] private Transform _viewTransform;
	[SerializeField] private Animator _animator;

	private Coroutine _coroutine;
	private RaycastHit2D _backVisionHit;
	private RaycastHit2D _forwardVisinHit;
	private int _currentIndex;
	private bool _isChase = false;

	public Health Health { get; private set; }

	private void Awake()
	{
		Health = GetComponent<Health>();
	}

	private void OnEnable()
	{
		_attackZone.Attacking += StartAttack;
	}

	private void OnDisable()
	{
		StopAttack();
	}

	private void Update()
	{
		_backVisionHit = Physics2D.Raycast(_viewTransform.position, Vector2.left, DetectionDistance, _playerMask);
		_forwardVisinHit = Physics2D.Raycast(_viewTransform.position, Vector2.right, DetectionDistance, _playerMask);

		if (_backVisionHit || _forwardVisinHit)
		{
			_isChase = true;
		}
		else
		{
			_isChase = false;
		}

		Move();
	}

	private void Move()
	{
		Vector2 currentTarget;

		if (_isChase)
		{
			currentTarget = _playerTarget.position;
		}
		else
		{
			currentTarget = _patrolTargets[_currentIndex].position;
		}

		transform.position = Vector2.MoveTowards(transform.position, currentTarget, _speed * Time.deltaTime);
		Vector2 offset = _patrolTargets[_currentIndex].position - transform.position;

		if (offset.sqrMagnitude < Epsilon * Epsilon)
		{
			_currentIndex = (++_currentIndex) % _patrolTargets.Length;
		}

		ChangeDirection(currentTarget);
	}

	private void ChangeDirection(Vector2 currentTarget)
	{
		if (transform.position.x < currentTarget.x)
		{
			_viewTransform.rotation = Quaternion.Euler(0, 0, 0);
		}
		if (transform.position.x > currentTarget.x)
		{
			_viewTransform.rotation = Quaternion.Euler(0, 180, 0);
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
			player.Health.DecreaseValue(_damage);
			yield return wait;
		}
	}
}
