using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public ObjectPool objectPool;
    public GameObject projectilePrefab; // 원거리 투사체
    public Transform firePoint; // 투사체 발사 위치

    private Animator _animator;



    private void Awake()
    {
        attackRange = 10f;
        _animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        /*if(isCheckPlayer())
        {
            RangedAttack();
            _animator.SetBool("IsRangeAttack", true);
            lastAttackTime = Time.time;
            StartCoroutine(ResetAttackAnimation());
        }*/
        if (isCheckPlayer() && Time.time >= lastAttackTime + attackCooldown)
        {
            _animator.SetBool("IsRangeAttack", true);
            RangedAttack();
            lastAttackTime = Time.time;
            StartCoroutine(ResetAttackAnimation());
        }
    }

    private void RangedAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f); // 부드럽게 회전

            GameObject projectile = objectPool.GetObject();
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;

            //Rigidbody rb = projectile.GetComponent<Rigidbody>();
            //rb.velocity = (player.position - firePoint.position).normalized * 10f; // 투사체 속도
            Projectile fire = projectile.GetComponent<Projectile>();
            fire.Launch(player.position);
            fire.damage = this.AttackPower;

            lastAttackTime = Time.time;

            StartCoroutine(ReturnProjectileToPool(projectile, 2f));
        }
    }

    private IEnumerator ReturnProjectileToPool(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.ReturnObject(projectile);
    }


    private IEnumerator ResetAttackAnimation()
    {
        // 공격 애니메이션이 끝나는 데 걸리는 시간 (애니메이션 길이에 맞게 설정)
        yield return new WaitForSeconds(1.0f);

        // 공격 애니메이션 종료
        _animator.SetBool("IsRangeAttack", false);
    }


}
