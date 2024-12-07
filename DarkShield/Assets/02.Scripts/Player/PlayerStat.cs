using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
	private Player _player;
	[SerializeField] private SOPlayerData _playerData;

	//PlayerData
	public float playerDamage;
	public float criticalChance;
	public float criticalDamage { get; set; }
	public float CriticalDamage
	{
		get
		{
			return criticalDamage / 100f;
		}
	}

	//WeaponData
	private float weaponDamage => _player.curWeopon.damage;

	private void Awake()
	{
		Init();
	}

	public float GetFinalDamage(float skillDamage)
	{
		float totalDamage = playerDamage + weaponDamage + skillDamage;
		totalDamage = Critical(totalDamage); //크리티컬 판단 //true 이면 크뎀 적용, 아니면 총합한 데미지
		return totalDamage;
	}

	private float Critical(float damage)
	{
		float _finalDamage;
		float random = Random.Range(0f, 100f);
		if (criticalChance >= random)
			_finalDamage = damage * (1 + CriticalDamage);
		else
			_finalDamage = damage;
		return _finalDamage;
	}

	#region Init
	private void Init()
	{
		_player = GetComponent<Player>();
		SetPlayerData();
	}

	private void SetPlayerData()
	{
		playerDamage = _playerData.damage;
		criticalChance = _playerData.criticalChance;
		criticalDamage = _playerData.criticalDamage;
	}
	#endregion
}
