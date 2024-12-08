using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
	private Text _goldText;

	private void Awake()
	{
		_goldText = GetComponentInChildren<Text>();
	}

	private void Update()
	{
		_goldText.text = $"Gold: {GameManager.Instance.gold.Amount}";
	}
}
