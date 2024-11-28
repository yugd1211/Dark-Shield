using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    public Action dialogueEndAction;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        dialogueManager.dialogueEndAction = dialogueEndAction;
        dialogueManager.StartDialogue(dialogue);
    }
    
}