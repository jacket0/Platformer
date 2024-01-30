using System.Collections;
using UnityEngine;

public class GemsSpawner : MonoBehaviour
{
	[SerializeField] private float _waitSecond = 5f;
	[SerializeField] private Gem _gem;
	[SerializeField] private Transform[] _spawnPoints;

	private Gem _newGem;
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
		StopCoroutine(_coroutine);
		_newGem.GemDestroed -= CreateNewGem;
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
		_newGem = Instantiate(_gem, position, Quaternion.identity);
		_newGem.GemDestroed += CreateNewGem;
	}
}
