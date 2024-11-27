using TMPro.EditorUtilities;
using UnityEngine;

public class Stage : MonoBehaviour
{
	public Player player;
	public StageManager stageManager;
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
