using UnityEngine;
public class BattleStage : Stage
{
	public EnemySpawnData enemySpawnData;
	public Transform[] enemySpawnPoint;
	
	public override void Init(StageManager stageManager)
	{
		base.Init(stageManager);
	}
	
	public void BattleStart()
	{
		EnemyManager enemyManager = GameManager.Instance.enemyManager;
		enemySpawnData = GameManager.Instance.enemySpawnDatas[GameManager.Instance.stageManager.currentStageIndex];
		enemyManager.Init(enemySpawnData);
		enemyManager.enemySpawner.StartSpawning(enemySpawnPoint);
	}
}
