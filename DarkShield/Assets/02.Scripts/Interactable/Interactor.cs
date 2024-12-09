using System.Linq;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;
public class Interactor : MonoBehaviour
{
	public float detectionRadius = 2f;
	
	public void OnInteract(Context context)
	{
		if (!context.performed)
			return;
		Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
		IInteractable interactableObject = null;
		Collider detectedInteractableCollider = colliders.ToList().Find(collider => collider.TryGetComponent(out interactableObject));
		if (!detectedInteractableCollider)
			return;
		if (interactableObject.CanInteract())
			interactableObject.Interact(this);
	}
}
