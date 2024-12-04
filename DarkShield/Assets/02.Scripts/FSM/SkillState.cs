using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class SkillState : IState
{
    private Player _player;

    public SkillState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        if (_player.playerInputManager.IsRightMousePressed)
        {
            _player.curWeopon.UseSkill(ActionType.Skill2);
        }
        else if (_player.playerInputManager.IsQPressed)
        {
            _player.curWeopon.UseSkill(ActionType.Skill3);
        }
    }

    public void OnUpdate()
    {
        //Die
        if (_player.playerHealth.Death)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.dieState);
        }

        //ComboAttack
        if (_player.playerInputManager.ComboContext.performed && !_player.playerInputManager.IsNonCombo
            && _player.playerInputManager.ComboContext.control.name == "leftButton" && !_player.playerInputManager.IsComboTrigger)
        {
            _player.playerInputManager.count++;
            _player.playerInputManager.ComboTrigger(1); //1 == true,  0 == false
            _player.curWeopon.UseSkill(ActionType.Skill1);
        }
    }

    public void OnExit()
    {
        _player.playerInputManager.IsLeftMousePressed = false;
        _player.playerInputManager.IsRightMousePressed = false;
        _player.playerInputManager.IsQPressed = false;
        _player.playerInputManager.IsSkill = false;

        //Combo Init
        _player.playerInputManager.IsNonCombo = false;
        _player.playerInputManager.count = 0;
        _player.playerInputManager.ComboTrigger(0);
    }

    public void EndSkill()
    {
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }
}
