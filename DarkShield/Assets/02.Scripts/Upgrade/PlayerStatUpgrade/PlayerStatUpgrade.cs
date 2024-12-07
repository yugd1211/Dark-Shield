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
	private Player _player;

	private void Start()
	{
		ChildStatButtonInit();
		_player = FindObjectOfType<Player>();
	}

	public void StatUpgrade(Stat statType, int value)
	{
		switch (statType)
		{
			case Stat.Health:
				_player.playerHealth.maxHealth += value;
				print($"최대 체력 : {_player.playerHealth.maxHealth}");
				break;
			case Stat.CharacterDamage:
				_player.playerStat.playerDamage += value;
				print($"플레이어 공격력 : {_player.playerStat.playerDamage}");
				break;
			case Stat.WeaponDamage:
				_player.curWeopon.damage += value;
				print($"무기 공격력 : {_player.curWeopon.damage}");
				break;
			case Stat.CriticalChance:
				_player.playerStat.criticalChance += value;
				print($"크리티컬 확률 : {_player.playerStat.criticalChance}");
				break;
			case Stat.CriticalDamage:
				_player.playerStat.criticalDamage += value;
				print($"크리티컬 데미지 : {_player.playerStat.CriticalDamage}");
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
