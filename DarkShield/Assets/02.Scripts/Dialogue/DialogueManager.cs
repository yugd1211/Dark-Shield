using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public DialogueUI dialogueUI;
    
    private Queue<string> _sentences;
    private PlayerInput _playerInput;
    public Action dialogueEndAction;

    private void Awake()
    {
        _sentences = new Queue<string>();
    }

    private void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    
    private void OnInteract(InputAction.CallbackContext context)
    {
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();
        _playerInput.actions["UI/Interact"].performed += OnInteract;

        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        dialogueUI.SetDialogueText(sentence);
    }

    private void EndDialogue()
    {
        dialogueUI.HideDialogue();
        dialogueEndAction?.Invoke();
        _playerInput.actions["UI/Interact"].performed -= OnInteract;
    }
}