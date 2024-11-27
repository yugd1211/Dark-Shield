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

    public override void UseSkill1()
    {
        slashAttackPt.Play();
        _player.playerAnimator.SetTrigger("Skill1");
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
}
