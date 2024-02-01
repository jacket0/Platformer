using Cinemachine.Utility;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
	private const float RayDistance = 1f;

	private readonly string Horizontal = nameof(Horizontal);
	public readonly int Speed = Animator.StringToHash(nameof(Speed));
	public readonly int DoJump = Animator.StringToHash(nameof(DoJump));
	public readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
	public readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));

	[SerializeField] private int _health = 100;
	[SerializeField] private int _damage = 20;
	[SerializeField] private float _velocity = 1f;
	[SerializeField] private float _jumpForce = 1f;
	[SerializeField] private LayerMask _layerMask;

	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float direction = Input.GetAxis(Horizontal);
		bool hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, RayDistance, _layerMask);

		_animator.SetBool(IsFalling, !hit);
		SetDirection(direction);
		_animator.SetFloat(Speed, Mathf.Abs(direction));
		Vector2 vectorDirection = new Vector2(direction, 0f);

		//var project = Vector3.ProjectOnPlane(transform.position, hit.normal);

		//Debug.Log(project);
		transform.Translate(_velocity * vectorDirection * Time.deltaTime);
		Attack();
		//Debug.DrawRay(transform.position, project, Color.green);
		Jump();
	}

	private void SetDirection(float direction)
	{
		_spriteRenderer.flipX = direction < 0;
	}

	private void Jump()
	{
		var raycastHit2D = Physics2D.Raycast(_rigidbody.position, Vector2.down, RayDistance, _layerMask);

		if (Input.GetKeyDown(KeyCode.W) && raycastHit2D)
		{
			_animator.SetTrigger(DoJump);
			_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}
	}

	private void Attack()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_animator.SetTrigger(DoAttack);

		}

	}
}
