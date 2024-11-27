using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class NormalEnemy : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    NavMeshAgent agent; // NavMeshAgent
    public float detectionRange = 15f; // 플레이어 탐지 범위
    public float attackCooldown = 2f; // 공격 쿨타임

    public float attackRange = 1f;

    protected float lastAttackTime = 0f;

    public float health = 200; //현재 채력
    public float AttackPower = 20f;//공격력

    private Animator _animotor;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animotor = GetComponent<Animator>();
        
        player = GameObject.Find("Player").transform;
    }

    //근거리 애너미, 원거리 애너미 클래스로 만들기
    void Update()
    {
        Move();
        Attack();
        // 1. move > move, attack 함수로 나누기
        // 2. move, attack, 추적
        // 
    }

    private void Move()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            agent.isStopped = false;// 플레이어를 추적
            agent.SetDestination(player.position);
            _animotor.SetBool("IsMoving",true);
        }
        else
        {
            agent.isStopped = true; // 탐지 범위를 벗어나면 정지
            _animotor.SetBool("IsMoving", false);
        }
    }
    protected bool isCheckPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown;
    }

    public abstract void Attack();
    public void TakeDamage(float damage)
    {
        _animotor.SetTrigger("Hit");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _animotor.SetTrigger("Death");
        Destroy(gameObject, 5f);
    }

}
