using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState
{
    private Player _player;

    public DashState(Player player)
    {
        _player = player;
    }
    public void OnEnter()
    {
        _player.playerMovement.Dash();

        _player.playerAnimator.SetTrigger("Dash");
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
        _player.playerInputManager.IsDash = false;
    }

    public void EndDash()
    {
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }
}
