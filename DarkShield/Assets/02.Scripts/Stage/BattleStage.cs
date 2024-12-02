using UnityEngine;
public class BattleStage : Stage
{
	public EnemySpawnData enemySpawnData;
	// EnemySpawner 객체로 변경
	public GameObject EnemySpawner;
	
	public override void Init(StageManager stageManager)
	{
		base.Init(stageManager);
		EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
		enemyManager.Init(enemySpawnData);
		enemyManager.StartSpawning();
		// EnemySpawner 객체 생성하고 Init과 Spawn 스케줄 관리
	}
}
