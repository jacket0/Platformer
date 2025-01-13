using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	private const float GroundDistance = 1.2f;

	private readonly string Horizontal = nameof(Horizontal);

	[SerializeField] private float _velocity = 1f;
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private Transform _viewTransform;
    [SerializeField] private PlayerAnimator _animator;

    private RaycastHit2D _groundHit;

	private Vector2 _boxCastSize = new Vector2(0.9f, 0.2f);
	private Rigidbody2D _rigidbody;
	private float _direction;
	private float _boxCastAngle = 0f;
	private bool _isJumped = false;

	public Vector2 LookDirection { get; private set; } = new Vector2(1, 0);

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_direction = Input.GetAxis(Horizontal);
		_groundHit = Physics2D.BoxCast(transform.position, _boxCastSize, _boxCastAngle, Vector2.down, GroundDistance, _groundLayer);
		_animator.PlayFalling(_groundHit);

		if (Input.GetKeyDown(KeyCode.W) && _groundHit)
			_isJumped = true;

		SetDirection();
		_animator.PlayRun(_direction);
		Move();
	}

	private void FixedUpdate()
	{
		Jump();
	}

	private void Jump()
	{
		if (_isJumped)
		{
			_animator.PlayJump();
			_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
			_isJumped = false;
		}
	}

	private void Move()
	{
		Vector2 vectorDirection = new Vector2(_direction, 0f);
		transform.Translate(_velocity * Time.deltaTime * vectorDirection);
	}

	private void SetDirection()
	{
		if (_direction < 0)
		{
			_viewTransform.rotation = Quaternion.Euler(0, 180, 0);
			LookDirection = new Vector2(-1, 0);
		}
		else if (_direction > 0)
		{
			_viewTransform.rotation = Quaternion.Euler(0, 0, 0);
			LookDirection = new Vector2(1, 0);
		}
	}
}
