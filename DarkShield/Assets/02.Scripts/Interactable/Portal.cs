using UnityEngine;
using System;

public class Portal : MonoBehaviour, IInteractable
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
		nextStageType = nextStage switch
		{
			BattleStage => StageType.Battle,
			ShopStage => StageType.Shop,
			BossStage => StageType.Boss,
			MussangStage => StageType.Mussang,
		};
		portalEffect[(int)nextStageType].SetActive(true);
	}

	private void Update()
	{
		if (CanInteract())
			GetComponent<MeshRenderer>().material.color = Color.green;
		else
			GetComponent<MeshRenderer>().material.color = Color.red;
	}

	public void Interact(Interactor player)
	{
		if (CanInteract())
		{
			_stageManager.stageList.Current.Value.MoveNextStage(nextStage);
			portalEffect[(int)nextStageType].SetActive(false);
		}
	}

	public bool CanInteract()
	{
		return OnCanInteract == null || OnCanInteract.Invoke();
	}
}
