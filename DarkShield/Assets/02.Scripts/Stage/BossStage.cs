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
	}
}
