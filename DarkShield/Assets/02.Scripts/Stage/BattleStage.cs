using System.Collections;
using UnityEngine;
public class BattleStage : Stage
{
	public EnemySpawnData enemySpawnData;
	public Transform[] enemySpawnPoint;
	
	public GameObject upgradeObjectPrefab;
	public GameObject upgradeObjectSpawnPoint;
	
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
		isStageCleared = false;
		StartCoroutine(BattleEndCoroutine());
	}

	private IEnumerator BattleEndCoroutine()
	{
		yield return new WaitUntil(() => GameManager.Instance.enemyManager.enemySpawner.isAllWavesCompleted);
		GameObject go = Instantiate(upgradeObjectPrefab, upgradeObjectSpawnPoint.transform.position, Quaternion.identity);
		yield return new WaitUntil(() => go == null);
		BattleEnd();
	}

	private void BattleEnd()
	{
		isStageCleared = true;
	}
}
