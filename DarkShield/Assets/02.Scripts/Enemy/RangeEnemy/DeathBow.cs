using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTargetGroup;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class DeathBow: EleteEnemy
{
    public float dashSpeed = 20f; // 돌진 속도 
    public float dashDuration = 0.5f; // 돌진 지속 시간
    private bool isDashing = false; // 돌진 상태 플래그
    private Vector3 dashDirection; // 돌진 방향

    public GameObject projectilePrefab; // 원거리 투사체
    public Transform firePoint; // 투사체 발사 위치

    public ObjectPool objectPool;

    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPatern();
            lastAttackTime = Time.time;
        }
    }

    private void AttackPatern()
    {
        // 일정 확률로 돌진 공격 수행
        if (Random.value > 0.5f) // 50% 확률
        {
            StartCoroutine(DashAttack());
        }
        else
        {
            PerformRangedAttack();
        }
    }

    IEnumerator DashAttack()
    {
        animotor.SetTrigger("DashAttack");

        isDashing = true;
        dashDirection = (player.position - transform.position).normalized;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null; // 한 프레임 대기
        }
        isDashing = false;
    }

    private void PerformRangedAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            animotor.SetTrigger("RangeAttack");


            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); // 부드럽게 회전

            GameObject projectile = objectPool.GetObject();
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;

            
            Projectile fire = projectile.GetComponent<Projectile>();
            fire.target = "Player";
            fire.Launch(player.position);
            this.AttackPower = fire.damage;

            lastAttackTime = Time.time;

            StartCoroutine(ReturnProjectileToPool(projectile, 5f));
        }
    }

    private IEnumerator ReturnProjectileToPool(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.ReturnObject(projectile);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && isDashing)
        {
            //print("돌진 공격 성공");
        }
    }
}
