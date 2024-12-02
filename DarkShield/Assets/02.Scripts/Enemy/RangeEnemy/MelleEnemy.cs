using UnityEngine;

public class MelleEnemy : Enemy
{
    private void Awake()
    {
        attackRange = 5f;
    }

    public override void Attack()
    {

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            MeleeAttack();
            lastAttackTime = Time.time;
        }
    }

    private void MeleeAttack()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); // 부드럽게 회전
        _animotor.SetTrigger("MelleAttack");
        print("근접 공격 실행!!");
    }

    public void ApplyDamage()
    {
        // 플레이어와의 거리 확인
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            // 플레이어에게 데미지 적용
            player.GetComponent<PlayerHealth>().TakeDamage(AttackPower);
            Debug.Log("플레이어에게 근접 공격 피해를 입혔습니다!");
        }
        else
        {
            print("플레이어가 공격 범위 밖에 있습니다.");
        }
    }

}
