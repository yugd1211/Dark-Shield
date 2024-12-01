public class BattleStage : Stage
{
	// Spawn에 대한 Data가 담김
	public EnemySpawnData enemySpawnData;
	
	public override void Init(StageManager stageManager)
	{
		base.Init(stageManager);
		EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
		enemyManager.Init(enemySpawnData);
		enemyManager.StartSpawning();
		// EnemySpawner 객체 생성하고 Init과 Spawn 스케줄 관리
	}
}
