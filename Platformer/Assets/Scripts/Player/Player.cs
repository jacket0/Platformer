using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
	[SerializeField] private int _damage = 20;
	[SerializeField] private LayerMask _enemyLayer;
	[SerializeField] private PlayerMovement _movement;
	[SerializeField] private Animator _animator;

	private const float AttackDistance = 2.5f;

	private readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	private RaycastHit2D _attackHit;
	private float _attackReloadTime = 1f;
	private float _lastAttackTime = 0f;

	public Health Health { get; private set; }

	private void Start()
	{
		Health = GetComponent<Health>();
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
