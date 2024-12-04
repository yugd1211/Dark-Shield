using UnityEngine;

public class MelleEnemy : Enemy
{
    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            MeleeAttack();
            lastAttackTime = Time.time;
        }
    }

    private void MeleeAttack()
    {
        animotor.SetTrigger("MelleAttack");
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); // 부드럽게 회전
        player.GetComponent<PlayerHealth>().TakeDamage(AttackPower, true);
        SetState(new EnemyIdleState(this));
    }

}
