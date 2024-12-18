using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
	public Player player;
	public StageManager stageManager;
	public List<Portal> portals = new List<Portal>();
	public List<Transform> portalPoints = new List<Transform>();
	public GameObject portalPrefab;
	public bool isStageCleared;
	
	// Init을 위에서 할당하다보니 인스펙터에서 할당해야함
	public Transform playerStartPos;
	public virtual void Init(StageManager stageManager)
	{
		this.stageManager = stageManager;
		player = stageManager.player;
		isStageCleared = true;
	}
	
	public void GoToStage()
	{
		if (!player)
			player = stageManager.player;
		if (player && playerStartPos)
			player.playerMovement.Spawn(playerStartPos.position);
	}

	public virtual void CreateNextPortal()
	{
		if (portalPrefab == null)
			return;
		Portal newPortal = Instantiate(portalPrefab, portalPoints[0].position, Quaternion.identity).GetComponent<Portal>();
		newPortal.transform.SetParent(transform);
		newPortal.Init();
		portals.Add(newPortal);
		portalPoints.RemoveAt(0);
		newPortal.OnCanInteract += () => isStageCleared;
	}

	public void MoveNextStage(Stage moveStage)
	{
		
		stageManager.MoveStage(moveStage);
		// stageManager.ChangeStage(moveStage);

	}
}
