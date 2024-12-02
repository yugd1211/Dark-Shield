using System.Collections.Generic;
using UnityEngine;

public class TriggerTakeDamage : MonoBehaviour
{
	public float damage;
	public List<Collider> colls;

	private void Awake()
	{
		colls = new List<Collider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IDamageable>(out IDamageable other2))
		{
			colls.Add(other);
			foreach (Collider coll in colls)
			{
				if (colls.Contains(coll))
				{
					continue;
				}
			}

			other2.TakeDamage(damage, false);
		}
	}
}
