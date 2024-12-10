using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public bool isAllWavesCompleted;
	
	private EnemyManager _enemyManager;
	private EnemySpawnData _enemySpawnData;
	private int _currentWave = 0;
	private bool _isSpawning = false;
	private Transform[] _enemySpawnPoint;
	
	public void Init(EnemyManager enemyManager, EnemySpawnData enemySpawnData)
	{
		_enemyManager = enemyManager;
		_enemySpawnData = enemySpawnData;
		isAllWavesCompleted = false;
	}

	private Coroutine _spawnCoroutine;

	public void StartSpawning(Transform[] enemySpawnPoint)
	{
		_enemySpawnPoint = enemySpawnPoint;
		if (_spawnCoroutine == null)
		{
			_spawnCoroutine = StartCoroutine(SpawnEnemies());
		}
	}

	private IEnumerator SpawnEnemies()
	{
		isAllWavesCompleted = false;
		while (_currentWave < _enemySpawnData.waves.Count)
		{
			yield return StartCoroutine(SpawnWave(_enemySpawnData.waves[_currentWave]));
			_currentWave++;
		}
		isAllWavesCompleted = true;
		_spawnCoroutine = null;
	}

	private IEnumerator SpawnWave(EnemySpawnWave wave)
	{
		foreach (EnemySpawnInfo enemyInfo in wave.enemies)
		{
			for (int i = 0; i < enemyInfo.count; i++)
			{
				Enemy newEnemy = Instantiate(enemyInfo.enemyPrefab, _enemySpawnPoint[Random.Range(0, _enemySpawnPoint.Length)].position, Quaternion.identity);
				newEnemy.AttackPower += (GameManager.Instance.stageManager.currentStageIndex / 10.0f) * newEnemy.AttackPower;
				newEnemy.maxHP += (GameManager.Instance.stageManager.currentStageIndex / 10.0f) * newEnemy.maxHP;
				newEnemy.health += (GameManager.Instance.stageManager.currentStageIndex / 10.0f) * newEnemy.health;
				print(newEnemy.maxHP);
				_enemyManager.AddEnemy(newEnemy);
				yield return new WaitForSeconds(wave.spawnFrequency);
			}
		}
		yield return new WaitUntil(() => _enemyManager.GetEnemies().Count == 0);
	}
}
