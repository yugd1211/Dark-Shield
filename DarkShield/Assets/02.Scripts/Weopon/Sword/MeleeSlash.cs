using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.ParticleSystem;

public class MeleeSlash : Skill
{
    public SOSkill meleeSlash1;
    public SOSkill meleeSlash2;
    public SOSkill meleeSlash3;

    private AnimationEventEffects _eventEffects;
    private AnimationEventEffects.EffectInfo _effect;
    private AnimationEventEffects.EffectInfo _effect2;
    private AnimationEventEffects.EffectInfo _effect3;
    private Transform _meleeSlashPivot;

    //스킬 업에 필요한 변수
    private float damagePercent;
    private float damagePercent2;
    private float damagePercent3;
    private GameObject meleeSlash3Fx;

    private bool _isElementSkill;
    public GameObject FireMeleeSlash3Fx;

    //====================================
    //오버랩 구현에 필요한 변수
    //====================================
    //ComboAttack1&2
    public Vector3 boxSize;
    public Vector3 boxOffset;
    public LayerMask layerMask;

    private Vector3 _boxCenter;
    private Transform _boxCenterPivot; // 박스 중심을 기준으로 할 피벗 Transform
    private Player _player;

    //ComboAttack3
    private Vector3 _sphereCenter;
    public float radius;

    //FireElement ComboAttack1&2
    //위에 ComboAttack1&2 변수들 ChangeElement에서 값 바꾸면 됌.
    //FireElement ComboAttack3 box2
    public Vector3 boxSize2;
    public Vector3 boxOffset2;
    private Vector3 _boxCenter2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SpecialUpgrade();
        _boxCenter = _boxCenterPivot.TransformPoint(boxOffset);
        _sphereCenter = _player.transform.position;

        _boxCenter2 = _boxCenterPivot.TransformPoint(boxOffset2);
    }

    // 기즈모로 박스 표시
    void OnDrawGizmos()
    {
        if (_boxCenterPivot == null) return;

        Gizmos.color = Color.green;

        // 박스 기즈모
        _boxCenter = _boxCenterPivot.TransformPoint(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(_boxCenter, _boxCenterPivot.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(Vector3.zero, boxSize);

        // 행렬 초기화
        Gizmos.matrix = Matrix4x4.identity;

        // 박스2 기즈모
        Gizmos.color = Color.blue;

        _boxCenter2 = _boxCenterPivot.TransformPoint(boxOffset2);
        Gizmos.matrix = Matrix4x4.TRS(_boxCenter2, _boxCenterPivot.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize2);
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawCube(Vector3.zero, boxSize2);

        // 행렬 초기화
        Gizmos.matrix = Matrix4x4.identity;

        // 구체 기즈모
        _sphereCenter = _player.transform.position; // 실시간 갱신
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sphereCenter, radius);
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(_sphereCenter, radius);
    }
    //====================

    public override void UseSkill()
    {
        if (_eventEffects.effects.Count == 0)
        {
            //print("이펙트 세팅");
            _eventEffects.SetEffects(_effect);
            _eventEffects.SetEffects(_effect2);
            _eventEffects.SetEffects(_effect3);
        }
    }

    public override void ChangeEffect(ElementChange element)
    {
        _isElementSkill = true;
        //Change Effects
        _effect.Effect = element.skillData[0].skillEffect;
        _effect2.Effect = element.skillData[1].skillEffect;
        _effect3.Effect = element.skillData[2].skillEffect;
        //Change Position&Rotation
        Transform[] childTransform = _meleeSlashPivot.GetComponentsInChildren<Transform>();
        _effect.StartPositionRotation = childTransform[1];
        _effect2.StartPositionRotation = childTransform[2];
        _effect3.StartPositionRotation = childTransform[3];
        //Add Element Damage
        _effect.damage += element.skillData[0].damage;
        _effect2.damage += element.skillData[1].damage;
        _effect3.damage += element.skillData[2].damage;
        //1,2 번째 공격 판정 범위 바꾸기
        radius = element.skillData[0].radius;
        _effect.targetScan = SkillSphereRange;
        _effect2.targetScan = SkillSphereRange;
        //3번째 마지막 공격 판정 범위 바꾸기
        boxSize2 = element.skillData[2].boxSize;
        boxOffset2 = element.skillData[2].boxOffset;
        _effect3.targetScan = Fire3SkillBoxRange;
    }

    public void SkillBoxRange(float damage)
    {
        Collider[] hitColliders = Physics.OverlapBox(_boxCenter, boxSize / 2, _boxCenterPivot.rotation, layerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            //Debug.Log("감지된 이름 " + hitCollider.name);
            //print("가한 데미지 : " + damage);
            hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
        }
    }
    public void Fire3SkillBoxRange(float damage)
    {
        Collider[] hitColliders = Physics.OverlapBox(_boxCenter2, boxSize2 / 2, _boxCenterPivot.rotation, layerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            //Debug.Log("감지된 이름 " + hitCollider.name);
            //print("가한 데미지 : " + damage);
            hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
        }
    }

    public void SkillSphereRange(float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(_sphereCenter, radius, layerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            //Debug.Log("감지된 이름 " + hitCollider.name);
            //print("가한 데미지 : " + damage);
            hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
        }
    }

    public override void DamageUpgrade()
    {
        damagePercent += 0.3f;
        damagePercent2 += 0.3f;
        damagePercent3 += 0.5f;
        _effect.damage *= damagePercent;
        _effect2.damage *= damagePercent2;
        _effect3.damage *= damagePercent3;
    }

    public override void SpecialUpgrade()
    {
        if (_isElementSkill)
        {
            //이펙트 바꿔주기
            _effect3.Effect = FireMeleeSlash3Fx;
            boxSize2.x = 1.5f;
        }
        else
        {        // 기존 랜덤 크기 범위 가져오기
            ParticleSystem.MainModule mainModule = meleeSlash3Fx.GetComponentInChildren<ParticleSystem>().main;
            float minSize = mainModule.startSize.constantMin;
            float maxSize = mainModule.startSize.constantMax;

            minSize *= 1.5f;
            maxSize *= 1.5f;
            radius *= 1.5f;

            // 새로운 랜덤 크기 설정
            mainModule.startSize = new ParticleSystem.MinMaxCurve(minSize, maxSize);
            _effect3.Effect = meleeSlash3Fx;
            Debug.Log($"파티클 크기 범위: {minSize} ~ {maxSize}");
        }
    }

    public override void Init(Player player)
    {
        _player = player;
        _eventEffects = player.GetComponent<AnimationEventEffects>();
        _meleeSlashPivot = GameObject.Find("MeleeSlashPivot").transform;
        meleeSlash1.startPositionRotation = _meleeSlashPivot;
        meleeSlash3.startPositionRotation = player.transform;
        _effect = new AnimationEventEffects.EffectInfo(meleeSlash1, SkillBoxRange);
        _effect2 = new AnimationEventEffects.EffectInfo(meleeSlash1, SkillBoxRange);
        _effect3 = new AnimationEventEffects.EffectInfo(meleeSlash3, SkillSphereRange);

        //ComboAttack1&2
        _boxCenterPivot = _meleeSlashPivot;
        boxSize = meleeSlash1.boxSize;
        boxOffset = meleeSlash1.boxOffset;
        //ComboAttack3
        radius = meleeSlash3.radius;

        damagePercent = meleeSlash1.damagePercent;
        damagePercent2 = meleeSlash2.damagePercent;
        damagePercent3 = meleeSlash1.damagePercent;
        meleeSlash3Fx = Instantiate(meleeSlash3.skillEffect, _player.transform.position, Quaternion.identity);
    }
}
