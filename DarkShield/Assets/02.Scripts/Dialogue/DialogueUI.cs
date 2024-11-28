using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    
    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        dialoguePanel = gameObject;
        dialoguePanel.SetActive(false);
    }
    
    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        _playerInput.SwitchCurrentActionMap("UI");
        dialoguePanel.SetActive(true);
    }

    public void HideDialogue()
    {
        _playerInput.SwitchCurrentActionMap("Player");   
        dialoguePanel.SetActive(false);
    }


}