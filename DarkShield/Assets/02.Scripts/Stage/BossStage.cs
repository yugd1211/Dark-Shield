using UnityEngine;
public class BossStage : Stage
{
	public EnemySpawnData enemySpawnData;
	public Transform[] bossSpawnPoint;
	
	public override void Init(StageManager stageManager)
	{
		base.Init(stageManager);
	}
	
	public void BattleStart()
	{
		EnemyManager enemyManager = GameManager.Instance.enemyManager;
		enemyManager.Init(enemySpawnData);
		enemyManager.enemySpawner.StartSpawning(bossSpawnPoint);
		// EnemySpawner 객체 생성하고 Init과 Spawn 스케줄 관리
		// EnemySpawner StartSpawning
	}
}
