using UnityEngine;
public class ShopObject : MonoBehaviour, IInteractable
{
	private ShopPresenter _shopPresenter;

	private void Start()
	{
		_shopPresenter = FindObjectOfType<ShopPresenter>();
	}
	
	public void Interact(Interactor player)
	{
		_shopPresenter.OpenShop();
	}
	public bool CanInteract()
	{
		return true;
	}

}
