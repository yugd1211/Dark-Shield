using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSlash : Skill
{
	private Collider _teleportSlashArea;
	private Weapon _weapon;

	private AnimationEventEffects _eventEffects;
	private AnimationEventEffects.EffectInfo _effect;
	private Transform _startPositionRotation;
	[SerializeField] private SOSkill _skillData;

	public float damage;

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		_weapon.SetSkill(this);
		_effect = new AnimationEventEffects.EffectInfo(_skillData, _startPositionRotation);
	}

	public override void UseSkill()
	{
		_eventEffects.SetEffects(_effect);
		StartCoroutine(UseTeleportSlash());
	}

	private IEnumerator UseTeleportSlash()
	{
		yield return new WaitForSeconds(0.1f);
		_teleportSlashArea.enabled = true;
		yield return new WaitForSeconds(0.3f);
		_teleportSlashArea.enabled = false;
		_teleportSlashArea.GetComponent<TriggerTakeDamage>().colls.Clear();
	}

	private void Init()
	{
		_teleportSlashArea = GameObject.Find("TeleportSlashCollider").GetComponent<Collider>();
		damage = _skillData.damage;
		_teleportSlashArea.GetComponent<TriggerTakeDamage>().damage = damage;
		_weapon = GetComponentInParent<Weapon>();
		_eventEffects = GameObject.Find("Player").GetComponent<AnimationEventEffects>();
		_startPositionRotation = _teleportSlashArea.transform;

		actionType = _skillData.ActionType;
	}
}
