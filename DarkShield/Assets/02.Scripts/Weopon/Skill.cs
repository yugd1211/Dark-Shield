using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
	public SkillType skillType;

	public abstract void UseSkill();
}

public enum SkillType
{
	Dash,
	Skill1,
	Skill2
}
