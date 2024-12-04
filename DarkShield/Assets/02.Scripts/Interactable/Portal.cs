using UnityEngine;
using System;

public class Portal : AInteractableObeject
{
	public event Func<bool> OnCanInteract;
	
	private StageManager _stageManager;

	public void Init()
	{
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
		if (CanInteract())
			_stageManager.currStage.MoveNextStage();
	}

	public override bool CanInteract()
	{
		return OnCanInteract == null || OnCanInteract.Invoke();
	}
}
