using UnityEngine;

public class Stage : MonoBehaviour
{
	public Player player;
	public StageManager stageManager;
	public Stage nextStage;
	public Transform playerStartPos;
	public Portal portal;


	public virtual void Init(StageManager stageManager)
	{
		this.stageManager = stageManager;
		player = stageManager.player;
		playerStartPos = transform.Find("PlayerStartPosition").transform;
		if (player == true && playerStartPos == true)
			player.playerMovement.Spawn(playerStartPos.position);
		portal = GetComponentInChildren<Portal>();
		if (this as ShopStage)
			portal.OnCanInteract += () => true;
		else if (this as BattleStage)
			portal.OnCanInteract += () => GameManager.Instance.enemyManager.enemySpawner.isAllWavesCompleted;
		portal.Init();
	}
	
	public void MoveNextStage()
	{
		stageManager.ChangeStage(nextStage);
	}
}
