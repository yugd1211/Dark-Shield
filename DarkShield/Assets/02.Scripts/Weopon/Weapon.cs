using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	protected Dictionary<SkillType, Skill> skills;

	protected virtual void Awake()
	{
		skills = new Dictionary<SkillType, Skill>();
	}

	public abstract void UseSkill(SkillType skillType);

	public void SetSkill(Skill skill)
	{
		if (skills.ContainsKey(skill.skillType))
		{
			Debug.LogError($"중복된 스킬 타입입니다. {skill.skillType}");
		}
		skills.Add(skill.skillType, skill);
	}
}
