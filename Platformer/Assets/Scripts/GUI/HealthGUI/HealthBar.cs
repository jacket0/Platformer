using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBar : MonoBehaviour
{
	[SerializeField] private Health _value;

    protected abstract void UpdateValue(float health, float maxHealth);

    private void OnEnable()
	{
		_value.ValueChanged += UpdateValue;
	}

	private void OnDisable()
	{
		_value.ValueChanged -= UpdateValue;
	}
}
