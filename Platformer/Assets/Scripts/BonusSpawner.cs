using System.Collections;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
	[SerializeField] private float _waitSecond = 5f;
	[SerializeField] private Item _bonus;
	[SerializeField] private Transform[] _spawnPoints;

	private Item _newBonus;
	private Coroutine _coroutine;
	private int _currentPoint = 0;

	private void Start()
	{
		for (int i = 0; i < _spawnPoints.Length; i++)
		{
			CreateGem(_spawnPoints[i].position);
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		_newBonus.ItemPicked -= CreateNewGem;
	}

	private void CreateNewGem(Vector2 position)
	{
		_coroutine = StartCoroutine(CreateWithDelay(position));
	}

	private IEnumerator CreateWithDelay(Vector2 position)
	{
		yield return new WaitForSeconds(_waitSecond);
		CreateGem(position);
		_currentPoint = (++_currentPoint) % _spawnPoints.Length;
	}

	private void CreateGem(Vector2 position)
	{
		_newBonus = Instantiate(_bonus, position, Quaternion.identity);
		_newBonus.ItemPicked += CreateNewGem;
	}
}
