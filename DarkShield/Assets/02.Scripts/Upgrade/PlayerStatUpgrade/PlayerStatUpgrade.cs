using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
	Health,
	CharacterDamage,
	WeaponDamage,
	CriticalChance,
	CriticalDamage
}

public class PlayerStatUpgrade : MonoBehaviour
{
	private void Awake()
	{
		ChildStatButtonInit();
	}

	public void StatUpgrade(Stat statType)
	{
		switch (statType)
		{
			case Stat.Health:
				break;
			case Stat.CharacterDamage:
				break;
			case Stat.WeaponDamage:
				break;
			case Stat.CriticalChance:
				break;
			case Stat.CriticalDamage:
				break;
			default:
				break;
		}
	}

	public void ChildStatButtonInit()
	{
		StatButton[] statButton = GetComponentsInChildren<StatButton>();
		foreach (StatButton button in statButton)
		{
			button.Init(this);
		}
	}
}
