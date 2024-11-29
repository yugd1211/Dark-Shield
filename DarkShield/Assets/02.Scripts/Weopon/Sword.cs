using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Skill을 구현할 로직 : 지금 구체적으로 생각이 안나서 나중에 구현할 로직이기 때문에 주석처리.
//public abstract class Skill
//{

//    public string animName;
//    public void UseSkill()
//    {

//    }
//}

public class Sword : Weapon
{
    private Player _player;

    private Slash _slash;
    private TeleportSlash _teleportSlash;

    private void Awake()
    {
        Init();
    }


    public override void UseSkill1()
    {
        _player.playerAnimator.SetTrigger("Skill1");
        _slash.UseSkill();
    }

    public override void UseSkill2()
    {
        _player.playerAnimator.SetTrigger("Skill2");
        _teleportSlash.UseSkill();
    }

    //스킬이 구현되어 있지 않아서 주석 처리.
    //public void UseSkill(Skill skill)
    //{
    //    _player.playerAnimator.SetTrigger(skill.animName);
    //    skill.UseSkill();
    //}

    private void Init()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _player.curWeopon = this;
        _slash = GetComponentInChildren<Slash>();
        _teleportSlash = GetComponentInChildren<TeleportSlash>();
    }
}
