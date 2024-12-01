using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : IState
{
	private Player _player;

	public SkillState(Player player)
	{
		_player = player;
	}

	public void OnEnter()
	{
		if (_player.playerInputManager.IsLeftMousePressed)
		{
			_player.curWeopon.UseSkill(ActionType.Skill1);
		}
		else if (_player.playerInputManager.IsRightMousePressed)
		{
			_player.curWeopon.UseSkill(ActionType.Skill2);
		}
	}

	public void OnUpdate()
	{
		//Die
		if (_player.playerHealth.Death)
		{
			_player.playerStateMachine.TransitionTo(_player.playerStateMachine.dieState);
		}
	}

	public void OnExit()
	{
		_player.playerInputManager.IsLeftMousePressed = false;
		_player.playerInputManager.IsRightMousePressed = false;
		_player.playerInputManager.IsSkill = false;
	}

	public void EndSkill()
	{
		_player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
	}

}
