using TMPro;
using UnityEngine;

public class TextHealthBar : HealthBar
{
	[SerializeField] private TextMeshProUGUI _text;

	protected override void UpdateHealth(float health, float maxHealth)
	{
		_text.text = $"{health}/{maxHealth}";
	}
}
