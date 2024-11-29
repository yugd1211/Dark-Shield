using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour, IStructure
{
	public float damage;
	public float damageInterval;
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

	private void Update()
	{
		foreach (var target in _targets)
		{
			if (target.Value >= damageInterval)
			{
				target.Key.TakeDamage(damage);
				_targets[target.Key] = 0;
			}
			else
			{
				_targets[target.Key] += Time.deltaTime;
			}
		}
	}

	public void Affect(Unit target) { }

	public void OnTargetEnter(Unit target)
	{
		_targets[target] = 0;
	}

	public void OnTargetExit(Unit target)
	{
		_targets.Remove(target);
	}
}
