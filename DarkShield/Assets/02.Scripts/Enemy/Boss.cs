using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
 
public abstract class Boss : Enemy
{

    public float rangedAttackRange = 10f; //원거리 공격 범위

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    public abstract override void Attack();

    
}
