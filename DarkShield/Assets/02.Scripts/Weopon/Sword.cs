using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sword : Weapon
{
    public ParticleSystem slashAttackPt;
    public ParticleSystem skillAttackPt;
    public AnimationClip skill1Clip;
    public AnimationClip skill2Clip;
    private Player _player;

    //(Temp)Sword 공격 관련 변수들
    public int damage; //공격력
    public Vector3Int attackRange; //공격 사정 거리
    public LayerMask targetLayer; //타겟 레이어
    public Collider[] colls; //감지된 적
    public Vector2 boxCenter;

    private void Awake()
    {
        Init();
    }
    private void Start()
    {

        skill1Interval = skill1Clip.length;
        skill2Interval = skill2Clip.length;
        print(skill1Interval);
        print($"skill2Interval {skill2Interval}");
    }
    private void FixedUpdate()
    {
        //Temp
        TargetScan(transform.forward);
    }

    public override void UseSkill1()
    {
        slashAttackPt.Play();
        _player.playerAnimator.SetTrigger("Skill1");
        //Temp
        if(colls != null)
        {
            foreach(Collider coll in colls) 
            {
                coll.GetComponent<MelleEnemy>().TakeDamage(damage);
                // _player.playerAnimator.SetTrigger("Skill1");
                print($"데미지 받은 적 : {coll.name} 남은 체력 : {coll.GetComponent<NormalEnemy>().health}");
                // slashAttackPt.Play();
            }
        }
    }

    public override void UseSkill2()
    {
        skillAttackPt.Play();
        _player.playerAnimator.SetTrigger("Skill2");
    }

    private void Init()
    {
        _player = GetComponent<Player>();
    }

    //Temp
    //적 감지 메서드
    public void TargetScan(Vector2 dir)
    {
        boxCenter = (Vector2)transform.position + dir * 4; // 박스 Pivot dir으로 4만큼 이동
        colls = Physics.OverlapBox(boxCenter,attackRange,Quaternion.identity,targetLayer);
    }
}
