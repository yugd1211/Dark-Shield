using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatButton : MonoBehaviour
{
	public Stat statType;
	[SerializeField] private int _cost;
	[SerializeField] private int _value;

	private PlayerStatUpgrade _statUpgrade;
	private Button _button;
	private TextMeshProUGUI _text; //Stat_value Description
	private TextMeshProUGUI _costText;
	private StringBuilder _strBuilder;

	private void CanStatUpgrade()
	{
		if (GameManager.Instance.gold.Amount < _cost) return;

		AddCost();
		StatUpgrade();
	}

	private void StatUpgrade()
	{
		_statUpgrade.StatUpgrade(statType, _value);
	}

	private void AddCost()
	{
		_cost += 10;

		_strBuilder.Clear();
		_strBuilder.Append($"COST <color=red>{_cost}</color>");
		_costText.text = _strBuilder.ToString();
	}

	public void Init(PlayerStatUpgrade statUpgrade)
	{
		_statUpgrade = statUpgrade;
		_cost = 50;

		TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
		_text = texts[0];
		_costText = texts[1];

		_text.text += $"<color=green> +{_value}</color>";
		_costText.text += $"<color=red> {_cost}</color>";

		_strBuilder = new StringBuilder(_costText.text);
		_button = GetComponent<Button>();
		_button.onClick.AddListener(CanStatUpgrade);
	}
}
