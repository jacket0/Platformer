using UnityEngine;

public class ItemsSearch : MonoBehaviour
{
	private Player _player;

	private void Start()
	{
		_player = GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Healing healing))
		{
			healing.InvokePickedEvent();
			_player.Health.IncreaseHealth(healing.ExecuteHealing());
		}
		else if (collision.TryGetComponent(out Gem gem))
		{
			gem.InvokePickedEvent();
			gem.CollectGemBonus();
		}
	}
}
