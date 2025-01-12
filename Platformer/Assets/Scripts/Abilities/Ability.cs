using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _reloadTime = 4f;

    public bool IsAvailable { get; protected set; } = true;
    public Player Player => _player;
    public float Duration => _duration;
    public float ReloadTime => _reloadTime;

    protected abstract void SwitchActive();
}
