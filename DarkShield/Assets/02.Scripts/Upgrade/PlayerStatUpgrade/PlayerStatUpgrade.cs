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
				print($"�ִ� ü�� : {_player.playerHealth.maxHealth}");
				break;
			case Stat.CharacterDamage:
				_player.playerStat.playerDamage += value;
				print($"�÷��̾� ���ݷ� : {_player.playerStat.playerDamage}");
				break;
			case Stat.WeaponDamage:
				_player.curWeopon.damage += value;
				print($"���� ���ݷ� : {_player.curWeopon.damage}");
				break;
			case Stat.CriticalChance:
				_player.playerStat.criticalChance += value;
				print($"ũ��Ƽ�� Ȯ�� : {_player.playerStat.criticalChance}");
				break;
			case Stat.CriticalDamage:
				_player.playerStat.criticalDamage += value;
				print($"ũ��Ƽ�� ������ : {_player.playerStat.CriticalDamage}");
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
