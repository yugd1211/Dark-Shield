using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    private Player _player;

    public DieState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.playerAnimator.SetTrigger("Die");
        _player.Die();
    }


    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }

}
