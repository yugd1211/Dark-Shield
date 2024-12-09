using UnityEngine;
using System;

public class Portal : AInteractableObeject
{
	public enum StageType
	{
		Battle,
		Shop,
		Boss,
		Mussang
	}
	
	public Stage nextStage;
	public event Func<bool> OnCanInteract;
	public GameObject[] portalEffect;
	public StageType nextStageType;
	
	private StageManager _stageManager;

	public void Init()
	{
		_stageManager = GameManager.Instance.stageManager;
		nextStage = _stageManager.CreateStage();
		if (nextStage is BattleStage)
			nextStageType = StageType.Battle;
		else if (nextStage is ShopStage)
			nextStageType = StageType.Shop;
		else if (nextStage is BossStage)
			nextStageType = StageType.Boss;
		else if (nextStage is MussangStage)
			nextStageType = StageType.Mussang;
		portalEffect[(int)nextStageType].SetActive(true);
	}

	private void Update()
	{
		if (CanInteract())
			GetComponent<MeshRenderer>().material.color = Color.green;
		else
			GetComponent<MeshRenderer>().material.color = Color.red;
	}

	public override void Interact(Interactor player)
	{
		if (CanInteract())
		{
			_stageManager.currStage.MoveNextStage(nextStage);
			portalEffect[(int)nextStageType].SetActive(false);
		}
	}

	public override bool CanInteract()
	{
		return OnCanInteract == null || OnCanInteract.Invoke();
	}
}
