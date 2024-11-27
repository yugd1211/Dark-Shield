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
	//�ϳ��� ��ų �����
	//��ų Ŭ��, ��ų ��ƼŬ, ��ų������
	public Element elementType;
	public string skillName;
	public float skillDamage;
	//public int level;
	//public float skillDamage[];
	public AnimationClip skillClip;
	public GameObject skillPrefab;
}
