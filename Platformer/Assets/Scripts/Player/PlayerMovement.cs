using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private const float GroundDistance = 1.2f;

	private readonly string Horizontal = nameof(Horizontal);

	public readonly int Speed = Animator.StringToHash(nameof(Speed));
	public readonly int DoJump = Animator.StringToHash(nameof(DoJump));
	public readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));

	[SerializeField] private float _velocity = 1f;
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private LayerMask _groundLayer;

	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private RaycastHit2D _groundHit;
	private Vector2 _boxCastSize = new Vector2(0.9f, 0.2f);
	private float _direction;
	private float _boxCastAngle = 0f;
	private bool _isJumped = false;

	public Vector2 LookDirection { get; private set; } = new Vector2(1, 0);

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		_direction = Input.GetAxis(Horizontal);
		_groundHit = Physics2D.BoxCast(_rigidbody.position, _boxCastSize, _boxCastAngle, Vector2.down, GroundDistance, _groundLayer);

		if (Input.GetKeyDown(KeyCode.W) && _groundHit)
			_isJumped = true;

		_animator.SetBool(IsFalling, !_groundHit);

		SetDirection();
		_animator.SetFloat(Speed, Mathf.Abs(_direction));

		Moving();
	}

	private void FixedUpdate()
	{
		Jump();
	}

	private void Jump()
	{
		if (_isJumped)
		{
			_animator.SetTrigger(DoJump);
			_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
			_isJumped = false;
		}
	}

	private void Moving()
	{
		Vector2 vectorDirection = new Vector2(_direction, 0f);
		transform.Translate(_velocity * Time.deltaTime * vectorDirection);
	}

	private void SetDirection()
	{
		var rotation = transform.rotation;

		if (_direction < 0)
		{
			rotation = Quaternion.Euler(0, 180, 0);
			_direction *= -1;
			LookDirection = new Vector2(-1, 0);
		}
		else if (_direction > 0)
		{
			rotation = Quaternion.Euler(0, 0, 0);
			LookDirection = new Vector2(1, 0);
		}

		transform.rotation = rotation;
	}
}
