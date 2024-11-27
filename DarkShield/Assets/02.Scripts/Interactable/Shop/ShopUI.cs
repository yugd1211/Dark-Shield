using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUI : MonoBehaviour
{
    public PlayerInput _playerInput;
    
    private void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        HideShop();
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
}
