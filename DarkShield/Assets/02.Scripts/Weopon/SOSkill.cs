using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSkill", menuName = "ScriptableObject/Skill")]
public class SOSkill : ScriptableObject
{
	public enum Element
	{
		None, FIRE, ELEC, WATER, LIGHT, DARK
	}
	//하나의 스킬 만들기
	//스킬 클립, 스킬 파티클, 스킬데미지
	public Element elementType;
	public string skillName;
	public float skillDamage;
	//public int level;
	//public float skillDamage[];
	public AnimationClip skillClip;
	public GameObject skillPrefab;
}
