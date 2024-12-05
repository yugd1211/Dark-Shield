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
    public bool IsQPressed { get; set; }

    //ComboAttack 변수들
    public bool IsNonCombo { get; set; }
    public int count { get; set; }
    public bool IsComboTrigger { get; set; }
    public bool firstComboAttack { get; set; }
    public Context ComboContext;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        firstComboAttack = true;
    }

    public void OnMove(Context context)
    {
        InputMoveDir = context.ReadValue<Vector2>();
    }

    public void OnDash(Context context)
    {
        if (context.performed && !IsDash && _player.playerDash.CanDash() && !IsSkill)
        {
            IsDash = true;
        }
    }

    public void OnSkill(Context context)
    {
        if (!_player.isEquip) return;
        //Combo Attack
        if (context.performed && count < 3 && context.control.name == "leftButton" && !IsNonCombo && !IsComboTrigger
            && firstComboAttack)
        {
            firstComboAttack = false;
            ComboContext = context;
            IsSkill = true;
            IsLeftMousePressed = true;
        }

        if (context.performed && !IsSkill)
        {
            IsSkill = true;
            IsNonCombo = true;

            if (context.control.name == "rightButton")
            {
                IsRightMousePressed = true;
            }
            else if (context.control.name == "q")
            {
                IsQPressed = true;
            }
        }
    }

    //애니메이션 이벤트 메서드에서 사용함.
    public void ComboTrigger(int number)
    {
        IsComboTrigger = (number == 0) ? false : true;
    }
}