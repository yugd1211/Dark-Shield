using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public EnemySpawner enemySpawner;
	
	private readonly List<Enemy> _enemies = new List<Enemy>();
	
	public List<Enemy> GetEnemies() => new List<Enemy>(_enemies);

	public void Init(EnemySpawnData enemySpawnData)
	{
		if (enemySpawner)
		{
			print("Destroy EnemySpawner");
			Destroy(enemySpawner);
		}
		enemySpawner = gameObject.AddComponent<EnemySpawner>();
		enemySpawner.Init(this, enemySpawnData);
	}

	public void AddEnemy(Enemy newEnemy)
	{
		_enemies.Add(newEnemy);
	}
	
	public void RemoveEnemy(Enemy enemy)
	{
		_enemies.Remove(enemy);
	}

}
