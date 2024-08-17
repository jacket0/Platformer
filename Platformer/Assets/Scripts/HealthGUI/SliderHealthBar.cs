using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : HealthBar
{
	[SerializeField] private Slider _slider;

	protected override void UpdateHealth(int health, int maxHealth)
	{
		_slider.value = (float)health/maxHealth;
	}
}
