using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Skill
{
    [SerializeField] private Collider _slashColl; //키고 끔을 제어해야 할 변수
    private void Awake()
    {
        Init();
    }

    public override void UseSkill(Player player)
    {
        _slashColl.enabled = true;
    }

    protected override void Init()
    {
        base.Init();
        _slashColl = GetComponentInParent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is null)
        {
            print("감지된 적이 없습니다.");
            return;
        }
        other.GetComponent<EnemyTest>().TakeDamage(skillDamage);
        skillFX.Play();
    }
}
