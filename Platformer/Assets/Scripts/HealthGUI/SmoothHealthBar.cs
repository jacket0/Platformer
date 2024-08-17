using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthBar
{
	[SerializeField] private Slider _slider;
	[SerializeField] private float _fillingSpeed;

	private Coroutine _coroutine;

	protected override void UpdateHealth(int health, int maxHealth)
	{
		if (_coroutine != null)
			StopCoroutine(_coroutine);

		_coroutine = StartCoroutine(SmoothFill(health, maxHealth));
	}

	private void OnDisable()
	{
		if (_coroutine != null)
			StopCoroutine(_coroutine);
	}

	private IEnumerator SmoothFill(int health, int maxHealth)
	{
		float goal = (float)health / maxHealth;

		while (_slider.value != goal)
		{
			_slider.value = Mathf.MoveTowards(_slider.value, goal, _fillingSpeed * Time.deltaTime);
			yield return null;
		}

	}
}
