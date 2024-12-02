using UnityEngine;
using System;
using System.Collections;

public class Portal : AInteractableObeject
{
	public event Func<bool> OnCanInteract;

	private IEnumerator Start()
	{
		yield return null;
		// battleStage
		// 초기화 시 이벤트 구독
		Init();
		// StageManager.Instance.currStage.OnCanInteract += () => CanInteract();
	}

	public void Init()
	{
		OnCanInteract += () => FindObjectOfType<EnemyManager>().enemySpawner.isAllWavesCompleted;
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
			StageManager.Instance.currStage.MoveNextStage();
	}

	public override bool CanInteract()
	{
		return OnCanInteract == null || OnCanInteract.Invoke();
	}
}
