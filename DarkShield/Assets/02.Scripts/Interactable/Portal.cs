using UnityEngine;
using System;
using System.Collections;

public class Portal : AInteractableObeject
{
	public event Func<bool> OnCanInteract;
	
	private StageManager _stageManager;

	// private IEnumerator Start()
	// {
	// 	yield return null;
	// 	yield return null;
	// 	yield return null;
	// 	// battleStage
	// 	// 초기화 시 이벤트 구독
	// 	Init();
	// 	print("Portal Init");
	// 	// StageManager.Instance.currStage.OnCanInteract += () => CanInteract();
	// }

	public void Init()
	{
		OnCanInteract += () => { return GameManager.Instance.enemyManager.enemySpawner.isAllWavesCompleted;};
		_stageManager = GameManager.Instance.stageManager;
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
		print($"Portal Interact {GameManager.Instance.enemyManager.enemySpawner.isAllWavesCompleted}");
		if (CanInteract())
			_stageManager.currStage.MoveNextStage();
	}

	public override bool CanInteract()
	{
		return OnCanInteract == null || OnCanInteract.Invoke();
	}
}
