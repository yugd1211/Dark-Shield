using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SOSkill;

public abstract class Skill
{
	[SerializeField] private SOSkill _skillData;
	public Element elementType;
	public string skillName;
	public float skillDamage;
	public AnimationClip skillClip;
	public GameObject skillPrefab;

	public abstract void UseSkill();

	protected virtual void Init()
	{
		elementType = _skillData.elementType;
		skillName = _skillData.skillName;
		skillDamage = _skillData.skillDamage;
		skillClip = _skillData.skillClip;
		skillPrefab = _skillData.skillPrefab;
	}
}
