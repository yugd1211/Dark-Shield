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

	//임시 변수들
	public bool IsNonCombo { get; set; }
	public int count;
	public int performedCount;

	private Player player;
	public Context ComboContext;

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
		//Combo Attack
		if (context.performed && count <= 3 && context.control.name == "leftButton" && !IsNonCombo)
		{
			ComboContext = context;
			count++;
			IsSkill = true;
			IsLeftMousePressed = true;
		}

		if (context.performed && !IsSkill)
		{
			ResetComboState();

			IsSkill = true;
			IsNonCombo = true;
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

	public void ResetComboState()
	{
		IsLeftMousePressed = false;
		count = 0;
		performedCount = 0;
		ComboContext = default;
		IsNonCombo = false;
		// Debug.Log("Combo state reset.");
	}
}