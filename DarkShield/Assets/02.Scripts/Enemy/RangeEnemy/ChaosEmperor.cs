using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosEmperor : Boss
{
    public float dashSpeed = 20f; // 돌진 속도
    public float dashDuration = 0.5f; // 돌진 지속 시간
    
    private bool isDashing = false; // 돌진 상태 플래그
    private Vector3 dashDirection; // 돌진 방향
    
    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPattern();
            lastAttackTime = Time.time;
        }
    }

    private void AttackPattern() // 공격 패턴 함수
    {
        float randomValue = Random.value;
        if (Random.value > 0.5f)
        {
            StartCoroutine(DashAttack());
        }
        else
        {
            PerformRangedAttack();
        }
    }

    IEnumerator DashAttack() //대쉬 공격 메서드
    {
        yield return new WaitForSeconds(2.0f);
        animotor.SetTrigger("Dash");
        isDashing = true;
        dashDirection = (player.position - transform.position).normalized;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null; // 한 프레임 대기
        }
        print("원거리 대쉬 공격 실행");
        this.AttackPower = 40;
        player.GetComponent<PlayerHealth>().TakeDamage(AttackPower, true);
        isDashing = false;
    }


    private void PerformRangedAttack() //원거리 투사체 공격 메서드
    {

        animotor.SetTrigger("RangeAttack");
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); // 부드럽게 회전

        player.GetComponent<PlayerHealth>().TakeDamage(AttackPower, true);
        SetState(new EnemyIdleState(this));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && isDashing)
        {
            print("돌진 공격 성공");
        }
    }
}
