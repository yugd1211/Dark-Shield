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
        _player.playerAnimator.SetBool("IsSkill", true);
        if (_player.playerInputManager.IsRightMousePressed)
        {
            _player.curWeopon.UseSkill(ActionType.Skill2);
        }
        else if (_player.playerInputManager.IsRPressed)
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


        //AnimatorStateInfo stateInfo = _player.playerAnimator.GetCurrentAnimatorStateInfo(0);

        //if ((stateInfo.IsName("Skill1") || stateInfo.IsName("Combo2") || stateInfo.IsName("Combo3")) && stateInfo.normalizedTime >= 0.7f)
        //{
        //	_player.playerAnimator.ResetTrigger("Skill1");
        //}
        //ComboAttack
        else if (_player.playerInputManager.ComboContext.performed && !_player.playerInputManager.IsNonCombo
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
        _player.playerInputManager.IsRPressed = false;
        _player.playerInputManager.IsSkill = false;

        //Combo Init
        _player.playerInputManager.IsNonCombo = false;
        _player.playerInputManager.count = 0;
        _player.playerInputManager.ComboTrigger(0);
        _player.playerInputManager.firstComboAttack = true;
    }

    public void EndSkill()
    {
        _player.playerAnimator.SetBool("IsSkill", false);
        _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
    }
}
