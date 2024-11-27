using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1State : IState
{
    private Player _player;

    public Skill1State(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.curWeopon.UseSkill1();
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
        _player.playerInputManager.IsSkill1 = false;
    }

    public void EndSkill1()
    {
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }

}
