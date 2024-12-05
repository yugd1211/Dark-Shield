using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
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

        AnimatorStateInfo stateInfo = _player.playerAnimator.GetCurrentAnimatorStateInfo(0);
        Debug.Log(stateInfo.IsName("Combo2"));
        if ((stateInfo.IsName("Skill1") || stateInfo.IsName("Combo2") || stateInfo.IsName("Combo3")) && stateInfo.normalizedTime >= 0.9f)
        {
            _player.playerAnimator.ResetTrigger("Skill1");
        }

        //ComboAttack
        if (_player.playerInputManager.ComboContext.performed && !_player.playerInputManager.IsNonCombo
            && _player.playerInputManager.ComboContext.control.name == "leftButton" && !_player.playerInputManager.IsComboTrigger)
        {
            if (_player.playerStateMachine.CurState != _player.playerStateMachine.skillState) return;
            _player.playerInputManager.count++;
            _player.playerInputManager.ComboTrigger(1); //1 == true,  0 == false
            _player.curWeopon.UseSkill(ActionType.Skill1);
        }
    }

    public void OnExit()
    {
        _player.playerAnimationEventEffects.EndEffect();
        _player.playerInputManager.IsLeftMousePressed = false;
        _player.playerInputManager.IsRightMousePressed = false;
        _player.playerInputManager.IsQPressed = false;
        _player.playerInputManager.IsSkill = false;

        //Combo Init
        _player.playerInputManager.IsNonCombo = false;
        _player.playerInputManager.count = 0;
        _player.playerInputManager.ComboTrigger(0);
        _player.playerInputManager.firstComboAttack = true;
    }

    public void EndSkill()
    {
        //Debug.Log("EndSkill이 호출 됌.");
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }
}
