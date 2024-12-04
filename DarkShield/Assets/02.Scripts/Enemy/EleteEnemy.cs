using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class EleteEnemy : Enemy
{ 
    public float rangedAttackRange = 10f;

    protected override void Start()
    {
        base.Start();
        /*CoinPick coinpick = GetComponent<CoinPick>();
        if (coinpick != null)
        {
            coinpick.coinValue = 600; // 코인 값 설정
        }*/
    }


    public abstract override void Attack();
}
