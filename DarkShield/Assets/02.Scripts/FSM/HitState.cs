using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : IState
{
    private Player _player;
    public HitState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.playerAnimator.SetTrigger("Hit");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        _player.playerHealth.IsHit = false;
    }

    public void EndHit()
    {
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }
}
