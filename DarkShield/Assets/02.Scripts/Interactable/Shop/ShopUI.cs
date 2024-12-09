using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
	private PlayerInput _playerInput;
	private Button _closeButton;

	private void Start()
	{
		_playerInput = FindObjectOfType<PlayerInput>();
		HideShop();

		_closeButton = GetComponentInChildren<Button>();
		_closeButton.onClick.AddListener(HideShop);
	}

	public void ShowShop()
	{
		_playerInput.SwitchCurrentActionMap("UI");
		gameObject.SetActive(true);
	}

	public void HideShop()
	{
		_playerInput.SwitchCurrentActionMap("Player");
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		_closeButton.onClick.RemoveListener(HideShop);
	}
}
