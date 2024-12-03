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

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void OnMove(Context context)
    {
        InputMoveDir = context.ReadValue<Vector2>();
    }

    public void OnDash(Context context)
    {
        if (context.performed && !IsDash && player.playerDash.CanDash())
        {
            IsDash = true;
        }
    }

    public void OnSkill(Context context)
    {
        if (context.performed && !IsSkill)
        {
            IsSkill = true;

            //if (context.control.name == "leftButton")
            //{
            //    IsLeftMousePressed = true;
            //}
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
}