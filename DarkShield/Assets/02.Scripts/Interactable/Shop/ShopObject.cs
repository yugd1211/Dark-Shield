public class ShopObject : AInteractableObeject
{
	private ShopPresenter _shopPresenter;

	private void Start()
	{
		_shopPresenter = FindObjectOfType<ShopPresenter>();
	}
	
	public override void Interact(Interactor player)
	{
		_shopPresenter.OpenShop();
	}
	
}
