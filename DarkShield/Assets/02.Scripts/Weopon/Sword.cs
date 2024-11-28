using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sword : Weapon
{
	[SerializeField] private Player _player;

	private Slash _slash;

	private void Awake()
	{
		Init();
	}

	public override void UseSkill1()
	{
		_player.playerAnimator.SetTrigger("Skill1");
		_slash.UseSkill();
	}

	public override void UseSkill2()
	{
		_player.playerAnimator.SetTrigger("Skill2");
	}

	private void Init()
	{
		_player = GameObject.Find("Player").GetComponent<Player>();
		_player.curWeopon = this;
		_slash = GetComponentInChildren<Slash>();
	}
}
