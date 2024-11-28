using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private Player _player;

    //public Skill slashSkill;

    public SlashTest slashTest;

    private void Awake()
    {
        Init();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _player.curWeopon = this;
        slashTest = GetComponent<SlashTest>();
    }

    public override void UseSkill1()
    {
        _player.playerAnimator.SetTrigger("Skill1");
        slashTest.UseSkill();
    }

    public override void UseSkill2()
    {
        _player.playerAnimator.SetTrigger("Skill2");
    }

    private void Init()
    {
        //_player = GetComponent<Player>();
    }

    //public override void UseSkill(Player player, Skill skill)
    //{
    //    skill.UseSkill(player);
    //}
}
