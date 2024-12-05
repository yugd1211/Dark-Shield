using System.Collections;
using UnityEngine;

public class TeleportSlash : Skill
{
	private Collider _teleportSlashArea;
	private AnimationEventEffects _eventEffects;
	private AnimationEventEffects.EffectInfo _effect;
	private Transform _startPositionRotation;

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

	public override void Init(Player player)
	{
		_teleportSlashArea = GameObject.Find("TeleportSlashCollider").GetComponent<Collider>();
		damage = skillData.damage;
		_teleportSlashArea.GetComponent<TriggerTakeDamage>().damage = damage;
		_startPositionRotation = _teleportSlashArea.transform;

		_eventEffects = player.GetComponent<AnimationEventEffects>();
		//_effect = new AnimationEventEffects.EffectInfo(skillData, _startPositionRotation);
	}
}
