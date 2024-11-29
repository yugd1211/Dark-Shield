using System;
using UnityEngine;

public class ShopPresenter : MonoBehaviour
{
    private ShopUI _shopUI;
    private DialogueTrigger _dialogueTrigger;


    private void Awake()
    {
        _dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    private void Start()
    {
        _shopUI = FindObjectOfType<ShopUI>(true);
    }

    public void OpenShop()
    {
        // 일단 UI를 띄우는 것만 구현하지만, 
        // 추후에 dialogueText를 먼저띄우고 그 다음에 shopUI를 띄우는 방식으로 변경할 예정
        _dialogueTrigger.dialogueEndAction = _shopUI.ShowShop;
        _dialogueTrigger.TriggerDialogue();
    }

    public void CloseShop()
    {
        _shopUI.HideShop();
    }
}
