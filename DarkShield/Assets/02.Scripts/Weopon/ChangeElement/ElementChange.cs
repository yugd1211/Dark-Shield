using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChange : MonoBehaviour
{
	public SOSkill[] skillData;
	public ActionType actionType;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player))
		{
			player.ChangeElement(this);
		}
	}
}
