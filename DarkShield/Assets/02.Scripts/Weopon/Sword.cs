using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sword : Weapon
{
	private Player _player;

	protected override void Awake()
	{
		base.Awake();
		Init();
	}

	public override void UseSkill(ActionType skillType)
	{
		_player.playerAnimator.SetTrigger(skillType.ToString());
		if (skills.TryGetValue(skillType, out Skill skill))
		{
			skill.UseSkill();
		}
	}

	private void Init()
	{
		_player = GameObject.Find("Player").GetComponent<Player>();
		_player.curWeopon = this;
	}
}
