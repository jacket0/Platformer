using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private const float AttackDistance = 2.5f;

    [SerializeField] private int _damage = 20;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerAnimator _animator;

    private RaycastHit2D _attackHit;
    private float _attackReloadTime = 1f;
    private float _lastAttackTime = 0f;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _input.Attacked += Attack;
    }

    private void OnDisable()
    {
        _input.Attacked -= Attack;
    }

    private void Update()
    {
        _attackHit = Physics2D.Raycast(transform.position, _movement.LookDirection, AttackDistance, _enemyLayer);
        _lastAttackTime += Time.deltaTime;
        Debug.DrawRay(transform.position, _movement.LookDirection * AttackDistance, Color.red);
    }

    private void Attack()
    {
        if (_lastAttackTime < _attackReloadTime)
            return;

        _animator.PlayAttack();
        _lastAttackTime = 0;

        if (_attackHit.collider == null)
            return;

        if (_attackHit.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.DecreaseValue(_damage);
        }
    }
}
