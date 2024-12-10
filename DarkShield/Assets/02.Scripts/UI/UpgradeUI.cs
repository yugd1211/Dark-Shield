using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeUI : MonoBehaviour
{
    public List<SkillUpgradeButton> buttonPrefabs;
    private PlayerInput _playerInput;
    private List<GameObject> _buttons = new List<GameObject>();

    private void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        HideShop();
    }

    private List<int> RandomNumbers(int count, int maxValue)
    {
        HashSet<int> uniqueNumbers = new HashSet<int>();
        while (uniqueNumbers.Count < count)
        {
            int randomNumber = Random.Range(0, maxValue);
            uniqueNumbers.Add(randomNumber);
        }
        return uniqueNumbers.ToList();
    }

    private void CreateButtons()
    {
        List<int> ran = RandomNumbers(3, buttonPrefabs.Count);
        for (int i = 0; i < ran.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefabs[ran[i]], transform).gameObject;
            button.GetComponent<SkillUpgradeButton>().Init(this);
            _buttons.Add(button);
        }
    }
    
    private void ClearButtons()
    {
        foreach (GameObject button in _buttons)
        {
            Destroy(button);
        }
        _buttons.Clear();
    }

    public void ShowShop()
    {
        ClearButtons();
        _playerInput.SwitchCurrentActionMap("UI");
        CreateButtons();
        gameObject.SetActive(true);
    }

    public void HideShop()
    {
        _playerInput.SwitchCurrentActionMap("Player");
        gameObject.SetActive(false);
    }
}
