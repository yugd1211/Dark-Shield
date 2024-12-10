using System.Linq;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Interactor : MonoBehaviour
{
	public float detectionRadius = 2f;
	private Canvas _canvas;
	private GameObject _indicator;

	private void Start()
	{
		_canvas = FindObjectOfType<Canvas>();
		_indicator = GameObject.Find("InteractableIndicator");
		_indicator.SetActive(false);
		if (_canvas == null)
		{
			Debug.LogError("Canvas not found in the scene.");
		}
	}

	private void Update()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
		IInteractable interactableObject = null;
		Collider detectedInteractableCollider = colliders.ToList().Find(collider => collider.TryGetComponent(out interactableObject));
		if (!detectedInteractableCollider)
		{
			_indicator.SetActive(false);
			return;
		}
		if (!interactableObject.CanInteract())
		{
			_indicator.SetActive(false);
			return;
		}
		// if (_canvas && !_indicator)
		// 	_indicator = Instantiate(interactableIndicator, _canvas.transform);
		
		if (_indicator)
		{
			Vector3 worldPosition = detectedInteractableCollider.transform.position;
			Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, screenPosition, _canvas.worldCamera, out Vector2 localPoint);
			((RectTransform)_indicator.transform).localPosition = localPoint + new Vector2(0, 100);
			_indicator.SetActive(true);
			
		}
	}

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
