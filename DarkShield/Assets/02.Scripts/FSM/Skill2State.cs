using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2State : IState
{
    private Player _player;

    public Skill2State(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.curWeopon.UseSkill2();
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
        _player.playerInputManager.IsSkill2 = false;
    }

    public void EndSkill2()
    {
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }
}
