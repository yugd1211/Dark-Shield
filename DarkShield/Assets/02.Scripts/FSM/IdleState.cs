using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleState : IState
{
    private Player _player;

    public IdleState(Player player)
    {
        this._player = player;
    }
    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        //Die
        if (_player.playerHealth.Death)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.dieState);
        }

        //Skill1
        if (_player.playerInputManager.IsSkill1)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.skill1State);
        }
        //Skill2
        else if (_player.playerInputManager.IsSkill2)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.skill2State);
        }
        //Dash
        else if (_player.playerInputManager.IsDash)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.dashState);
        }
        //Hit
        else if (_player.playerHealth.IsHit)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.hitState);
        }
        //Move
        else if (_player.playerInputManager.InputMoveDir.magnitude >= 0.1f)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.walkState);
        }
    }

    public void OnExit()
    {

    }

}
