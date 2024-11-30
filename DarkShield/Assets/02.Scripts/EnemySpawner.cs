using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public List<Enemy> enemys = new List<Enemy>();

	private EnemySpawnData _enemySpawnData;
	private int _currentWave = 0;
	
	
	public void Init(EnemySpawnData enemySpawnData)
	{
		_enemySpawnData = enemySpawnData;
	}
	
	public void StartSpawning()
	{
		StartCoroutine(SpawnEnemies());
	}
	
	private IEnumerator SpawnEnemies()
	{
		// list가 비어있으면 소환 해야함
		// 해당 wave의 소환이 끝나면 currentWave 증가
		// currentWave가 wave의 갯수보다 많아지면 소환 끝
		// 마지막에 Spawn이 끝났음을 알리든 bool값으로 갖고 있든 해야함
		foreach (EnemySpawnWave wave in _enemySpawnData.waves)
		{
			foreach (EnemySpawnInfo enemyInfo in wave.enemies)
			{
				for (int i = 0; i < enemyInfo.count; i++)
				{
					Instantiate(enemyInfo.enemyPrefab, transform.position, Quaternion.identity);
					yield return new WaitForSeconds(wave.spawnFrequency);
				}
			}
		}
	}
}
