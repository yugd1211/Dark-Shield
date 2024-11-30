using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private EnemyManager _enemyManager;

	private EnemySpawnData _enemySpawnData;
	private int _currentWave = 0;
	private bool _isSpawning = false;

	public void Init(EnemyManager enemyManager, EnemySpawnData enemySpawnData)
	{
		_enemyManager = enemyManager;
		_enemySpawnData = enemySpawnData;
	}

	private Coroutine _spawnCoroutine;

	public void StartSpawning()
	{
		if (_spawnCoroutine == null)
		{
			_spawnCoroutine = StartCoroutine(SpawnEnemies());
		}
	}

	private IEnumerator SpawnEnemies()
	{
		while (_currentWave < _enemySpawnData.waves.Count)
		{
			EnemySpawnWave wave = _enemySpawnData.waves[_currentWave];

			foreach (EnemySpawnInfo enemyInfo in wave.enemies)
			{
				for (int i = 0; i < enemyInfo.count; i++)
				{
					Enemy newEnemy = Instantiate(enemyInfo.enemyPrefab, transform.position, Quaternion.identity);
					_enemyManager.AddEnemy(newEnemy);
					yield return new WaitForSeconds(wave.spawnFrequency);
				}
			}
			yield return new WaitUntil(() => _enemyManager.GetEnemies().Count == 0);
			_currentWave++;
		}

		_spawnCoroutine = null;
	}
}
