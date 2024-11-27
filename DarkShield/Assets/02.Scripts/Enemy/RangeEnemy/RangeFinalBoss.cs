using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFinalBoss : Boss
{
    public float dashSpeed = 20f; // 돌진 속도
    public float dashDuration = 0.5f; // 돌진 지속 시간
    private bool isDashing = false; // 돌진 상태 플래그
    private Vector3 dashDirection; // 돌진 방향

    // 원거리 공격 관련 변수
    public GameObject projectilePrefab; // 원거리 투사체
    public Transform firePoint; // 투사체 발사 위치


    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPattern();
            lastAttackTime = Time.time;
        }
    }

    void AttackPattern() // 공격 패턴 함수
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
        _animation.SetTrigger("Dash");
        isDashing = true;
        dashDirection = (target.position - transform.position).normalized;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null; // 한 프레임 대기
        }
        print("원거리 대쉬 공격 실행");
        //this.AttackPower = 40;
        isDashing = false;
    }


    void PerformRangedAttack() //원거리 투사체 공격 메서드
    {

        _animation.SetTrigger("RangeAttack");
        Debug.Log("원거리 공격 실행!");

        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); // 부드럽게 회전
        // 투사체 생성
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (target.position - firePoint.position).normalized * 10f; // 투사체 속도;
        FinalSkill fire = projectile.GetComponent<FinalSkill>();
        this.AttackPower = fire.damage;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("돌진 공격 성공");
        }
    }
}
