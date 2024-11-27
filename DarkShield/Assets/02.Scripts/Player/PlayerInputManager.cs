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
    public bool IsSkill1 { get; set; }
    public bool IsSkill2 { get; set; }

    public void OnMove(Context context)
    {
        InputMoveDir = context.ReadValue<Vector2>();
    }

    public void OnDash(Context context)
    {
        if (context.performed && !IsDash)
        {
            IsDash = true;
        }
    }

    public void OnSkill1(Context context)
    {
        if (context.performed && !IsSkill1)
        {
            IsSkill1 = true;
        }
    }

    public void OnSkill2(Context context)
    {
        if (context.performed && !IsSkill2)
        {
            IsSkill2 = true;
        }
    }
}