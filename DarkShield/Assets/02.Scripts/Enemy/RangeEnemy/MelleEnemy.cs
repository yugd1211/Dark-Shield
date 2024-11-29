using UnityEngine;

public class MelleEnemy : Enemy
{

    private void Awake()
    {
        attackRange = 5f;
    }

    public override void Attack()
    {
        if (isCheckPlayer() && Time.time >= lastAttackTime + attackCooldown)
        {
            MeleeAttack();
            lastAttackTime = Time.time;
        }
    }

    private void MeleeAttack()
    {
        print("근접 공격 실행!!");
    }

   

}
