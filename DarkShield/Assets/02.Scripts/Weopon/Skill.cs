using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
	public ActionType skillType;

	public abstract void UseSkill();
}

public enum ActionType
{
	Dash,
	Skill1,
	Skill2
}
