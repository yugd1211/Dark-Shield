using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MeleeSlash : Skill
{
    private AnimationEventEffects _eventEffects;
    private AnimationEventEffects.EffectInfo _effect;
    private Transform _startPositionRotation;

    //Skill Damage 구현에 필요한 변수
    //오버랩 구현하면 사용하지 않을 변수
    private Collider _meleeSlashColl;
    private List<Collider> _damaged;
    private Player _player;
    //====================================
    //오버랩 구현에 필요한 변수
    //====================
    public Vector3 boxSize = new Vector3(2, 2, 2); // 박스 크기
    public Vector3 boxOffset = new Vector3(0, 0, 2); // 박스의 위치 (플레이어 기준)
    public LayerMask layerMask; // 특정 레이어만 감지하도록 설정 가능

    void Update()
    {
        // 플레이어 앞의 박스 위치 계산
        Vector3 boxCenter = transform.position + transform.forward * boxOffset.z
                            + transform.up * boxOffset.y
                            + transform.right * boxOffset.x;

        // 오버랩 박스를 사용해 충돌 감지
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, layerMask);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Detected: " + hitCollider.name);
        }
    }

    // 기즈모로 박스 표시
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // 박스 위치 계산
        Vector3 boxCenter = transform.position + transform.forward * boxOffset.z
                            + transform.up * boxOffset.y
                            + transform.right * boxOffset.x;

        // 박스를 기즈모로 그리기
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize); // WireCube: 선으로 된 박스
        Gizmos.color = new Color(0, 1, 0, 0.3f);    // 투명도 있는 녹색
        Gizmos.DrawCube(Vector3.zero, boxSize);     // Cube: 채워진 박스
    }
    //====================

    public override void UseSkill()
    {
        print("이펙트 세팅");
        if (_eventEffects.effects.Count == 0)
        {
            _eventEffects.SetEffects(_effect);
        }
        StartCoroutine(UseMeleeSlash());
    }

    private IEnumerator UseMeleeSlash()
    {
        yield return new WaitForSeconds(0.1f);
        _meleeSlashColl.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _meleeSlashColl.enabled = false;
        _damaged.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        //{
        //    if (!_damaged.Contains(other))
        //    {
        //        _damaged.Add(other);
        //        damageable.TakeDamage(_player.playerStat.GetFinalDamage(this), false);
        //    }
        //}
    }

    public void SkillRange()
    {
        //Physics.OverlapBox(,);
    }

    public override void Init(Player player)
    {
        _player = player;
        _eventEffects = player.GetComponent<AnimationEventEffects>();
        _startPositionRotation = GameObject.Find("MeleeSlashPivot").transform;
        _meleeSlashColl = GetComponent<Collider>();
        _effect = new AnimationEventEffects.EffectInfo(skillData, _startPositionRotation);
        _damaged = new List<Collider>();

        damage = skillData.damage;
        actionType = skillData.ActionType;
    }
}
