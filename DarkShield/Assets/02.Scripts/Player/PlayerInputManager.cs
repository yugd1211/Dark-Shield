using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 InputMoveDir { get; private set; }
    public bool IsDash { get; set; }
    public bool IsSkill { get; set; }

    public bool IsLeftMousePressed { get; set; }
    public bool IsRightMousePressed { get; set; }
    public bool IsRPressed { get; set; }

    //ComboAttack 변수들
    public bool IsNonCombo { get; set; }
    public int count { get; set; }
    public bool IsComboTrigger { get; set; }
    public bool firstComboAttack { get; set; }
    public Context ComboContext;
    public bool CanInput { get; set; }

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        firstComboAttack = true;
    }

    public void OnMove(Context context)
    {
        if (CanInput)
        { InputMoveDir = context.ReadValue<Vector2>(); }
    }

    public void OnDash(Context context)
    {
        if (context.performed && !IsDash && _player.playerDash.CanDash() && !IsSkill && CanInput)
        {
            IsDash = true;
        }
    }

    public void OnSkill(Context context)
    {
        if (!_player.isEquiped) return;
        //Combo Attack
        if (!IsDash && context.performed && count < 3 && context.control.name == "leftButton" && !IsNonCombo
            && firstComboAttack && (_player.playerStateMachine.CurState == _player.playerStateMachine.idleState
            || _player.playerStateMachine.CurState == _player.playerStateMachine.walkState))
        {
            //print("들어와짐");
            firstComboAttack = false;
            ComboContext = context;
            IsSkill = true;
            IsLeftMousePressed = true;
        }
        else if (context.performed && !IsSkill)
        {
            //print("들어와짐2");
            if (context.control.name == "rightButton")
            {
                IsRightMousePressed = true;
                IsSkill = true;
                IsNonCombo = true;
            }
            else if (context.control.name == "r")
            {
                IsRPressed = true;
                IsSkill = true;
                IsNonCombo = true;
            }
        }
    }

    //애니메이션 이벤트 메서드에서 사용함.
    public void ComboTrigger(int number)
    {
        IsComboTrigger = (number == 0) ? false : true;
    }
}