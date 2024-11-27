using UnityEngine;
public class AInteractableObeject : MonoBehaviour, IInteractable
{
	public virtual void Interact(Interactor player)
	{
		print("Interact");
		// Player Interact Animation Invoke
	}

	public virtual bool CanInteract()
	{
		return true;
	}
}
