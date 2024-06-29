using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
	private const float AttackDistance = 2.5f;

	public readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	[SerializeField] private int _damage = 20;
	[SerializeField] private LayerMask _enemyLayer;

	private RaycastHit2D _attackHit;
	private ItemsSearch _itemsSearch;
	private PlayerMovement _movement;
	private Animator _animator;
	private float _attackReloadTime = 1f;
	private float _lastAttackTime = 0f;

	public event Action<int> Attacking;

	public Health Health { get; private set; }

	private void Start()
	{
		_movement = GetComponent<PlayerMovement>();
		_itemsSearch = GetComponent<ItemsSearch>();
		Health = GetComponent<Health>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Attack();
	}

	private void Attack()
	{
		_attackHit = Physics2D.Raycast(transform.position, _movement.LookDirection, AttackDistance, _enemyLayer);
		Debug.DrawRay(transform.position, _movement.LookDirection * AttackDistance, Color.red);
		_lastAttackTime += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_lastAttackTime < _attackReloadTime)
				return;

			_animator.SetTrigger(DoAttack);
			_lastAttackTime = 0;

			if (_attackHit.collider == null)
				return;

			if (_attackHit.collider.TryGetComponent(out Enemy enemy))
			{
				enemy.Health.DecreaseHealth(_damage);
			}
		}
	}
}
