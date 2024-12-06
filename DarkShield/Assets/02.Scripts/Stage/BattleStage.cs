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
		// EnemySpawner 객체 생성하고 Init과 Spawn 스케줄 관리
		// EnemySpawner StartSpawning
	}
}
