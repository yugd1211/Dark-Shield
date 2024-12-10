using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCharge : MonoBehaviour, IInteractable
{
	private static int _cost = 50;
	[SerializeField] private float _recoveryAmount;

	private void HealthRecovery()
	{
		GameManager.Instance.player.playerHealth.AddHealth(_recoveryAmount);
		GameManager.Instance.gold.SubGold(_cost);
		Destroy(gameObject);
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
