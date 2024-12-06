using UnityEngine;

public class UpgradeObject : MonoBehaviour, IInteractable
{
	private UpgradeUI _upgradeUI;
	
	private void Start()
	{
		_upgradeUI = FindObjectOfType<UpgradeUI>(true);
	}

	public void Interact(Interactor player)
	{
		_upgradeUI.ShowShop();
		Destroy(gameObject);
	}
	
	public bool CanInteract() => true;
}
