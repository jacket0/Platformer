using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
	private const float RayDistance = 1f;

	private readonly string Horizontal = nameof(Horizontal);
	public readonly int Speed = Animator.StringToHash(nameof(Speed));

	[SerializeField] private float _velocity = 1f;
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private LayerMask _layerMask;

	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float direction = Input.GetAxis(Horizontal);

		SetDirection(direction);
		_animator.SetFloat(Speed, Mathf.Abs(direction));

		transform.Translate(_velocity * new Vector2(direction, 0f) * Time.deltaTime);
		Jump();
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
		var raycastHit2D = Physics2D.Raycast(_rigidbody.position, Vector2.down, RayDistance, _layerMask);

		if (Input.GetKeyDown(KeyCode.W) && raycastHit2D)
		{
			_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}
	}
}
