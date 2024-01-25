using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
	[SerializeField] private float _speed = 1f;
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private LayerMask _layerMask;

	private const float _rayDistance = 1f;

	private readonly string Horizontal = nameof(Horizontal);

	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		Jump();
	}

	private void FixedUpdate()
	{
		float direction = Input.GetAxis(Horizontal);

		SetDirection(direction);
		_animator.SetFloat("Speed", Mathf.Abs(direction));

		transform.Translate(_speed * new Vector2(direction, 0f) * Time.deltaTime);
	}

	private void SetDirection(float direction)
	{
		if (direction > 0)
		{
			_spriteRenderer.flipX = false;

		}
		else if (direction < 0)
		{
			_spriteRenderer.flipX = true;
		}
	}
	private void Jump()
	{
		var raycastHit2D = Physics2D.Raycast(_rigidbody.position, Vector2.down, _rayDistance, _layerMask);

		if (Input.GetKeyDown(KeyCode.W) && raycastHit2D)
		{
			_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}
	}
}
