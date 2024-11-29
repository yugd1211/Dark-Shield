using UnityEngine;

public class MelleEnemy : Enemy
{
    //public float meleeRange = 2f; // 근거리 공격 범위
    // 나도 필요없음 왜필요없을까?

    private void Awake()
    {
        attackRange = 5f;
    }

    public override void Attack()
    {
        /*if(isCheckPlayer())
        {
            MeleeAttack();
            lastAttackTime = Time.time;
        }*/
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
