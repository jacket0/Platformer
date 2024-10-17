using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBar : MonoBehaviour
{
	[SerializeField] private Health _health;

	private void OnEnable()
	{
		_health.ValueChanged += UpdateHealth;
	}

	private void OnDisable()
	{
		_health.ValueChanged -= UpdateHealth;
	}

	protected abstract void UpdateHealth(float health, float maxHealth);	
}
