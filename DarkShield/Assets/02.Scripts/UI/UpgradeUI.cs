using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeUI : MonoBehaviour
{
    public SkillUpgradeButton[] buttonPrefabs;
    
    private PlayerInput _playerInput;
    
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
