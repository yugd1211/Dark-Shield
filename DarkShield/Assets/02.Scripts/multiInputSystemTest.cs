using UnityEngine;
using UnityEngine.InputSystem;

public class multiInputSystemTest : MonoBehaviour
{
	private PlayerInput _playerInput;

	private void Start()
	{
		_playerInput = FindObjectOfType<PlayerInput>();
	}

	// private void OnEnable()
	// {
	// 	_playerInput.SwitchCurrentActionMap("UI");
	// }
	//
	// private void OnDisable()
	// {
	// 	_playerInput.SwitchCurrentActionMap("Player");
	// }
}
