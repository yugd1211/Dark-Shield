using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatButton : MonoBehaviour
{
	public Stat statType;
	[SerializeField] private int cost;

	private PlayerStatUpgrade _statUpgrade;
	private Button _button;

	private void CanStatUpgrade()
	{
		if (GameManager.Instance.gold.Amount < cost) return;

		AddCost();
		StatUpgrade();
	}

	private void StatUpgrade()
	{
		_statUpgrade.StatUpgrade(statType);
	}

	private void AddCost()
	{
		cost += 10;
	}

	public void Init(PlayerStatUpgrade statUpgrade)
	{
		_statUpgrade = statUpgrade;
		_button = GetComponent<Button>();

		_button.onClick.AddListener(CanStatUpgrade);
		cost = 50;
	}
}
