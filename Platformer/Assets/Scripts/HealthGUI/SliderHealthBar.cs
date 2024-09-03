using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : HealthBar
{
	[SerializeField] private Slider _slider;
	[SerializeField] private RectTransform _targetCanvas;
	[SerializeField] private Transform _followedObject;

	protected override void UpdateHealth(int health, int maxHealth)
	{
		_slider.value = (float)health / maxHealth;
	}
}
