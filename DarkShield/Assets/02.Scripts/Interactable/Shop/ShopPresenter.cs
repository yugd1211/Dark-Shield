using UnityEngine;

public class ShopPresenter : MonoBehaviour
{
    private ShopUI shopUI;

    private void Start()
    {
        shopUI = FindObjectOfType<ShopUI>(true);
    }

    public void OpenShop()
    {
        // 일단 UI를 띄우는 것만 구현하지만, 
        // 추후에 dialogueText를 먼저띄우고 그 다음에 shopUI를 띄우는 방식으로 변경할 예정
        shopUI.ShowShop();
    }

    public void CloseShop()
    {
        shopUI.HideShop();
    }
}
