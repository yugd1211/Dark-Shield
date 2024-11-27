using UnityEngine;

public class Stage : MonoBehaviour
{
	// 해당 객체들 구현 될 시 GameObject -> 해당 객체로 변경
	public Player player;
	public StageManager stageManager;
	//
	public Stage nextStage;
	public Transform playerStartPos;


	public virtual void Init(StageManager stageManager)
	{
		this.stageManager = stageManager;
		player = stageManager.player;
		playerStartPos = transform.Find("PlayerStartPosition").transform;
		if (player == true && playerStartPos == true)
			player.playerMovement.Spawn(playerStartPos.position);
	}
	
	public void MoveNextStage()
	{
		stageManager.ChangeStage(nextStage);
	}
}
