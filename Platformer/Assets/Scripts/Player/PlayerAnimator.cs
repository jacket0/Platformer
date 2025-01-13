using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int DoAttack = Animator.StringToHash(nameof(DoAttack));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));
    public readonly int DoJump = Animator.StringToHash(nameof(DoJump));
    public readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));

    public void PlayAttack()
    {
        _animator.SetTrigger(DoAttack);
    }

    public void PlayJump()
    {
        _animator.SetTrigger(DoJump);
    }

    public void PlayFalling(bool isGrounded)
    {
        _animator.SetBool(IsFalling, !isGrounded);
    }

    public void PlayRun(float direction)
    {
        _animator.SetFloat(Speed, Mathf.Abs(direction));
    }
}
