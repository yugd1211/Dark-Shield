using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleFinalBoss : FinalBoss
{
    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        Debug.Log("근거리 공격!");
        // 근거리 공격 로직 (예: 데미지 적용)
        lastAttackTime = Time.time;
    }
}
