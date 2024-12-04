using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour, IStructure
{
	public float decreaseSpeed;
	private Dictionary<Unit, float> _targets = new Dictionary<Unit, float>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Unit target))
			OnTargetEnter(target);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out Unit target))
			OnTargetExit(target);
	}

	public void Affect(Unit target) { }

	public void OnTargetEnter(Unit target)
	{
		float decreasedSpeed = target.MoveSpeed / decreaseSpeed;
		target.MoveSpeed -= decreasedSpeed;
		_targets[target] = decreasedSpeed;
	}

	public void OnTargetExit(Unit target)
	{
		if (_targets.ContainsKey(target))
		{
			target.MoveSpeed += _targets[target];
			_targets.Remove(target);
		}
	}
}
