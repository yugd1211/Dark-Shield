using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MeleeSlash : Skill
{
	public SOSkill meleeSlash1;
	public SOSkill meleeSlash3;

	private AnimationEventEffects _eventEffects;
	private AnimationEventEffects.EffectInfo _effect;
	private AnimationEventEffects.EffectInfo _effect3;
	private Transform _startPositionRotation;

	//====================================
	//오버랩 구현에 필요한 변수
	//====================================
	public Vector3 boxSize = new Vector3(2, 2, 2);
	public Vector3 boxOffset = new Vector3(0, 0, 2);
	public LayerMask layerMask;

	private Vector3 _boxCenter;
	private Transform _boxCenterPivot; // 박스 중심을 기준으로 할 피벗 Transform
	private Player _player;

	void Update()
	{
		_boxCenter = _boxCenterPivot.TransformPoint(boxOffset);
	}

	// 기즈모로 박스 표시
	void OnDrawGizmos()
	{
		if (_boxCenterPivot == null) return;

		Gizmos.color = Color.green;

		_boxCenter = _boxCenterPivot.TransformPoint(boxOffset);

		// 박스를 기즈모로 그리기
		Gizmos.matrix = Matrix4x4.TRS(_boxCenter, _boxCenterPivot.rotation, Vector3.one);
		Gizmos.DrawWireCube(Vector3.zero, boxSize); // WireCube: 선으로 된 박스
		Gizmos.color = new Color(0, 1, 0, 0.3f);    // 투명도 있는 녹색
		Gizmos.DrawCube(Vector3.zero, boxSize);     // Cube: 채워진 박스
	}
	//====================

	public override void UseSkill()
	{
		if (_eventEffects.effects.Count == 0)
		{
			//print("이펙트 세팅");
			_eventEffects.SetEffects(_effect);
			_eventEffects.SetEffects(_effect3);
		}
	}

	public void SkillRange(float damage)
	{
		Collider[] hitColliders = Physics.OverlapBox(_boxCenter, boxSize / 2, _boxCenterPivot.rotation, layerMask);

		foreach (Collider hitCollider in hitColliders)
		{
			Debug.Log("감지된 이름 " + hitCollider.name);
			print("가한 데미지 : " + damage);
			//hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
		}
	}

	public override void Init(Player player)
	{
		_player = player;
		_eventEffects = player.GetComponent<AnimationEventEffects>();
		_startPositionRotation = GameObject.Find("MeleeSlashPivot").transform;
		meleeSlash1.startPositionRotation = GameObject.Find("MeleeSlashPivot").transform;
		meleeSlash3.startPositionRotation = player.transform;
		_effect = new AnimationEventEffects.EffectInfo(meleeSlash1, SkillRange);
		_effect3 = new AnimationEventEffects.EffectInfo(meleeSlash3, SkillRange);

		damage = skillData.damage;

		_boxCenterPivot = GameObject.Find("MeleeSlashPivot").transform;
	}
}
