using Cinemachine.Utility;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
	private const float GroundDistance = 1.2f;
	private const float AttackDistance = 2.5f;

	private readonly string Horizontal = nameof(Horizontal);
	
	public readonly int Speed = Animator.StringToHash(nameof(Speed));
	public readonly int DoJump = Animator.StringToHash(nameof(DoJump));
	public readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
	public readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	[SerializeField] private int _health = 100;
	[SerializeField] private int _damage = 20;
	[SerializeField] private float _velocity = 1f;
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private LayerMask _enemyLayer;
	[SerializeField] private LayerMask _groundLayer;

	private Vector2 _lookDirection = new Vector2(1, 0);
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private RaycastHit2D _attackHit;

	public event Action<int> Attacking;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float direction = Input.GetAxis(Horizontal);
		RaycastHit2D groundHit = Physics2D.Raycast(_rigidbody.position, Vector2.down, GroundDistance, _groundLayer);

		_animator.SetBool(IsFalling, !groundHit);
		SetDirection(ref direction);
		_animator.SetFloat(Speed, Mathf.Abs(direction));
		Vector2 vectorDirection = new Vector2(direction, 0f);		

		transform.Translate(_velocity * vectorDirection * Time.deltaTime);

		Attack();
		Jump();
	}

	public void DecreaseHealth(int damage)
	{
		if (_health > 0)
		{
			_health -= damage;
			Debug.Log("Здоровье игрока: " + _health);
		}
	}

	public void IncreaseHealth(int heal)
	{
		if (_health < 100)
		{
			_health += heal;
		}

		if (_health + heal > 100)
		{
			_health = 100;
		}

		Debug.Log("Здоровье игрока: " + _health);
	}

	private void SetDirection(ref float direction)
	{
		var rotation = transform.rotation;

		if (direction < 0)
		{
			rotation = Quaternion.Euler(0, 180, 0);
			direction *= -1;
			_lookDirection = new Vector2(-1, 0);
		}
		else if (direction > 0) 
		{
			rotation = Quaternion.Euler(0, 0, 0);
			_lookDirection = new Vector2(1, 0);
		}

		transform.rotation = rotation;
	}

	private void Jump()
	{
		var raycastHit2D = Physics2D.Raycast(_rigidbody.position, Vector2.down, GroundDistance, _groundLayer);

		if (Input.GetKeyDown(KeyCode.W) && raycastHit2D)
		{
			_animator.SetTrigger(DoJump);
			_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}
	}

	private void Attack()
	{
		_attackHit = Physics2D.Raycast(transform.position, _lookDirection, AttackDistance, _enemyLayer);
		Debug.DrawRay(transform.position, _lookDirection * AttackDistance, Color.red);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			_animator.SetTrigger(DoAttack);

			if (_attackHit)
			{
				Attacking?.Invoke(_damage);
			}
		}
	}
}
