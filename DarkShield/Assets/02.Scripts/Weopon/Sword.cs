using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sword : Weapon
{
	public ParticleSystem slashAttackPt;
	public ParticleSystem skillAttackPt;
	public AnimationClip slashAttackClip;

	//private float lastNormalAttackTime;
	//private float lastSkillAttackTime;

	private void Start()
	{
		//lastNormalAttackTime = Time.time;
		//lastSkillAttackTime = Time.time;
		slashAttackInterval = slashAttackClip.length;
		print(slashAttackInterval);
	}

	//������ ��������
	//public override bool SlashAttack()
	//{
	//	if (Time.time >= lastNormalAttackTime && isAttack == false)
	//	{
	//		lastNormalAttackTime = normalAttackInterval + Time.time;
	//		StartCoroutine(NormalAttack());
	//		return true;
	//	}

	//	return false;
	//}

	//public override bool CanSkillAttack()
	//{
	//	if (Time.time >= lastSkillAttackTime && isAttack == false)
	//	{
	//		lastSkillAttackTime = skillAttackInterval + Time.time;
	//		StartCoroutine(SkillAttack());
	//		return true;
	//	}

	//	return false;
	//}

	//�⺻ ����
	public override void SlashAttack()
	{
		slashAttackPt.Play();
	}

	//��ų ����
	public override void SkillAttack()
	{
		skillAttackPt.Play();
	}
}
