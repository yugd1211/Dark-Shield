using UnityEngine;

public class WaveTest : MonoBehaviour
{ 
	private EnemySpawner _enemySpawner;
	public EnemySpawnData enemySpawnData;

	private void Awake()
	{
		_enemySpawner = gameObject.AddComponent<EnemySpawner>();
		_enemySpawner.Init(enemySpawnData);
	}
	
	private void Start()
	{
		_enemySpawner.StartSpawning();
	}
}
