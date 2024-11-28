using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour, IStructure
{
	public float decreaseSpeed;
	public float EffectAmount { get; set; }
	
	private Dictionary<IMovable, float> _targets = new Dictionary<IMovable, float>();
	
	private void Start()
	{
		EffectAmount = decreaseSpeed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IMovable target))
			OnTargetEnter(target);
	}

	public void Affect(IMovable target) { }
	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out IMovable target))
			OnTargetExit(target);
	}
	
	public void OnTargetEnter(IMovable target)
	{
		float decreasedSpeed = target.MoveSpeed / EffectAmount;
		target.MoveSpeed -= decreasedSpeed;
		_targets[target] = decreasedSpeed;
	}

	public void OnTargetExit(IMovable target)
	{
		if (_targets.TryGetValue(target, out float decreasedSpeed))
		{
			target.MoveSpeed += decreasedSpeed;
			_targets.Remove(target);
		}
	}
}
