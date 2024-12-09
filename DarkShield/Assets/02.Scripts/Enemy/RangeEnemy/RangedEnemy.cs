using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public ObjectPool objectPool;
    public GameObject projectilePrefab; // 원거리 투사체
    public Transform firePoint; // 투사체 발사 위치

    public float attackInterval;

    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public RectTransform hpBarForeground;

    protected override void Update()
    {
        base.Update();
        hpBarForeground.localScale = new Vector3(HpAmount, 1, 1);
    }

    public override void Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            _animator.SetTrigger("IsRangeAttack");
            StartCoroutine(RangedAttack());
            lastAttackTime = Time.time;
        }

    }

    private IEnumerator RangedAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z)); // y축은 고정
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360f); // 부드럽게 회전

            yield return new WaitForSeconds(attackInterval);
            GameObject projectile = objectPool.GetObject();
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;

            Projectile fire = projectile.GetComponent<Projectile>();
            fire.target = "Player";
            fire.Launch(player.position);
            fire.damage = this.AttackPower;

            lastAttackTime = Time.time;

            SetState(new EnemyIdleState(this));

            StartCoroutine(ReturnProjectileToPool(projectile, 2f));

        }
    }

    private IEnumerator ReturnProjectileToPool(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.ReturnObject(projectile);
    }
}
