using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public float maxHealth { get; set; }
	private float _health;

	[SerializeField] private SOPlayerData _playerData;
	public float HealthAmount
	{
		get
		{
			return _health / maxHealth;
		}
	}
	private Slider _playerHpbar;

	public bool Death
	{
		get
		{
			return _health <= 0;
		}
	}
	public bool IsHit { get; set; }
	private Player _player;

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		maxHealth = _playerData.health;
		_health = maxHealth;
	}

	private void Update()
	{
		_playerHpbar.value = HealthAmount;
	}

	public void TakeDamage(float damage, bool isHit)
	{
		_health -= damage;
		if (_health <= 0) _health = 0;

		//CurState == idleState || CurState == walkState && 디버프 지형이면 안가게
		if (_player.playerStateMachine.CanEnterHitState() && isHit)
		{
			IsHit = true;
		}
	}

	private void Init()
	{
		_player = GetComponent<Player>();
		_playerHpbar = GameObject.Find("PlayerHpBar").GetComponent<Slider>();
	}
}
