using TMPro;
using UnityEngine;

public class TextHealthBar : HealthBar
{
	[SerializeField] private TextMeshProUGUI _text;

	protected override void UpdateHealth(int health, int maxHealth)
	{
		_text.text = $"{health}/{maxHealth}";
	}
}
