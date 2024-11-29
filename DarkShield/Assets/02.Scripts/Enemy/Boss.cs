using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
 
public abstract class Boss : MonoBehaviour
{
    public Transform target; //플레이어 위치
    NavMeshAgent nmAgent; //navmeshagent
    public float detectionRange = 15f;//플레이어 탐지 범위
    public float meleeRange = 2f;//근거리 공격 범위

    public float attackRange = 1;

    public float health = 1000f;//현재 채력
    public float AttackPower = 20f;//공격력


    public float rangedAttackRange = 10f; //원거리 공격 범위
    public float attackCooldown = 2f;//공격 쿨타임
    protected float lastAttackTime = 0f;
    protected Animator _animation;

    public float maxHP = 1000f;
    public RectTransform hpBarForeground; // 초록색 HP 


    protected void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        _animation = GetComponent<Animator>();

    }
    private void Update()
    {
        Move();
        Attack();

    }

    private void Move()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            nmAgent.isStopped = false;// 플레이어를 추적
            nmAgent.SetDestination(target.position);
            _animation.SetBool("IsMoving", true);
           
        }
        else
        {
            nmAgent.isStopped = true; // 탐지 범위를 벗어나면 정지
            _animation.SetBool("IsMoving", false);
        }
    }

    public abstract void Attack();

    public void TakeDamage(float damage)
    {
        _animation.SetTrigger("Hit");
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHP);
        Debug.Log("Health after clamping: " + health);
        float hpPercent = health / maxHP;
        hpBarForeground.localScale = new Vector3(hpPercent, 1, 1); // 너비만 조정
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _animation.SetTrigger("Die");
        Destroy(gameObject);
    }
    
}
