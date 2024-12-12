using TMPro;
using UnityEngine;

public class HealthCharge : MonoBehaviour, IInteractable
{
	private static int _cost = 50;
	[SerializeField] private float _recoveryAmount;
	public TextMeshPro priceText;
	public TextMeshPro recoveryAmountText;

	public bool CanInteract()
	{
		if (GameManager.Instance.gold.Amount < _cost) return false;
		return true;
	}

	public void Interact(Interactor player)
	{
		HealthRecovery();
		AddCost();
		HideText();
	}
	
	private void HealthRecovery()
	{
		GameManager.Instance.player.playerHealth.AddHealth(
			(100 / _recoveryAmount) * GameManager.Instance.player.playerHealth.maxHealth);
		GameManager.Instance.gold.SubGold(_cost);
		Destroy(gameObject);
	}

	private void Update()
	{
		TextUpdate();
	}

	private void TextUpdate()
	{
		priceText.text = $"가격 : {_cost}";
		recoveryAmountText.text = $"회복량 : {_recoveryAmount}%";
	}

	private void AddCost()
	{
		_cost += 50;
		TextUpdate();
	}

	private void HideText()
	{
		priceText.gameObject.SetActive(false);
		recoveryAmountText.gameObject.SetActive(false);
	}
}
