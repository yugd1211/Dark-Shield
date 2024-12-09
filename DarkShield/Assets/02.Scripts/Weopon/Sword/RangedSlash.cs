using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSlash : Skill
{
	public SOSkill rangedSlash;
	public GameObject slashPrefab;
	private Player _player;
	private Transform rangedSlashPivot;

	private AnimationEventEffects _eventEffects;
	private AnimationEventEffects.EffectInfo _effect;
	//===========================
	//스킬 업에 필요한 변수
	private float damagePercent;
	private bool isSpecialUpgraged;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T)) SpecialUpgrade();
	}

	public override void UseSkill()
	{
		_eventEffects.SetEffects(_effect);
	}

	public void UseRangedSkill(float damage)
	{
		if (isSpecialUpgraged)
		{
			//파티클 생성
			//SlashPrefab 생성
			Quaternion rotationMinus = rangedSlashPivot.rotation * Quaternion.Euler(0, -15, 0);
			Quaternion rotationPlus = rangedSlashPivot.rotation * Quaternion.Euler(0, 15, 0);

			// -30도 회전 위치에 파티클 생성
			var effect1 = Instantiate(rangedSlash.skillEffect, -rangedSlashPivot.right + rangedSlashPivot.transform.position, rotationMinus);
			var slashInstance1 = Instantiate(slashPrefab, -rangedSlashPivot.right + rangedSlashPivot.transform.position, rotationMinus);
			SlashProjectile slashProjectile1 = slashInstance1.GetComponent<SlashProjectile>();
			slashProjectile1.Init(_player);
			slashProjectile1.SetDamage(damage);
			Destroy(effect1, rangedSlash.destroyAfter);

			// +30도 회전 위치에 파티클 생성
			var effect2 = Instantiate(rangedSlash.skillEffect, rangedSlashPivot.right + rangedSlashPivot.transform.position, rotationPlus);
			var slashInstance2 = Instantiate(slashPrefab, rangedSlashPivot.right + rangedSlashPivot.transform.position, rotationPlus);
			SlashProjectile slashProjectile2 = slashInstance2.GetComponent<SlashProjectile>();
			slashProjectile2.Init(_player);
			slashProjectile2.SetDamage(damage);
			Destroy(effect2, rangedSlash.destroyAfter);
		}
		GameObject slashInstance = Instantiate(slashPrefab, rangedSlashPivot.position, rangedSlashPivot.rotation);
		SlashProjectile slashProjectile = slashInstance.GetComponent<SlashProjectile>();
		slashProjectile.Init(_player);
		slashProjectile.SetDamage(damage);
	}

	public override void DamageUpgrade()
	{
		damagePercent += 0.3f;
		_effect.damage *= damagePercent;
	}

	public override void SpecialUpgrade()
	{
		isSpecialUpgraged = true;
	}

	public override void Init(Player player)
	{
		_player = player;
		_eventEffects = player.GetComponent<AnimationEventEffects>();
		rangedSlashPivot = GameObject.Find("RangedSlashPivot").GetComponent<Transform>();
		rangedSlash.startPositionRotation = rangedSlashPivot;

		_effect = new AnimationEventEffects.EffectInfo(rangedSlash, UseRangedSkill);

		damagePercent = rangedSlash.damagePercent;
	}
}
