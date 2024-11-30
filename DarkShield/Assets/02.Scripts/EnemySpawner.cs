using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public List<Enemy> enemys = new List<Enemy>();

	private EnemySpawnData _enemySpawnData;
	private int _currentWave = 0;
	private bool _isSpawning = false;

	public void Init(EnemySpawnData enemySpawnData)
	{
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

			foreach (var enemyInfo in wave.enemies)
			{
				for (int i = 0; i < enemyInfo.count; i++)
				{
					Instantiate(enemyInfo.enemyPrefab, transform.position, Quaternion.identity);
					yield return new WaitForSeconds(wave.spawnFrequency);
				}
			}

			// 해당 Wave의 적이 다 죽었는지 확인하기 위한 WaitUntil
			// yield return new WaitUntil(() => _isWaveComplete);
			_currentWave++;
		}

		_spawnCoroutine = null; // 코루틴이 끝났음을 표시
		Debug.Log("All waves have been spawned.");
	}
}
