using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCharge : MonoBehaviour, IInteractable
{
	[SerializeField] private int _cost;
	[SerializeField] private float _recoveryAmount;

	private void HealthRecovery()
	{
		GameManager.Instance.player.playerHealth.AddHealth(_recoveryAmount);
	}

	private void AddCost()
	{
		_cost += 50;
	}

	public bool CanInteract()
	{
		if (GameManager.Instance.gold.Amount < _cost) return false;
		return true;
	}

	public void Interact(Interactor player)
	{
		HealthRecovery();
		AddCost();
	}

}
