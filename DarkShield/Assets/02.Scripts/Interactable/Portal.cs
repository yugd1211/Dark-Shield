using UnityEngine;
using System;

public class Portal : AInteractableObeject
{
	public enum StageType
	{
		Battle,
		Shop,
		Boss,
		
	}
	
	public Stage nextStage;
	public event Func<bool> OnCanInteract;
	public GameObject[] portalEffect;
	
	private StageManager _stageManager;

	public void Init()
	{
		_stageManager = GameManager.Instance.stageManager;
		nextStage = _stageManager.CreateStage();
		if (nextStage is BattleStage)
			portalEffect[0].SetActive(true);
		else if (nextStage is ShopStage)
			portalEffect[1].SetActive(true);
		else if (nextStage is BossStage)
			portalEffect[2].SetActive(true);
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
			if (nextStage is BattleStage)
				portalEffect[0].SetActive(false);
			else if (nextStage is ShopStage)
				portalEffect[1].SetActive(false);
			else if (nextStage is BossStage)
				portalEffect[2].SetActive(false);
		}
	}

	public override bool CanInteract()
	{
		return OnCanInteract == null || OnCanInteract.Invoke();
	}
}
