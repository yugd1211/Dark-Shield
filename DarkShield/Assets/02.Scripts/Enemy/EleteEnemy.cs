using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class EleteEnemy : MonoBehaviour
{
    public Transform target;
    protected NavMeshAgent nmAgent;
    public float detectionRange = 15f;

    public float attackRange = 1;

    public float health = 800;
    public float attackpower = 40;
    protected Animator _animaton;


    public float rangedAttackRange = 10f;
    public float attackCooldown = 2f;   
    protected float lastAttackTime = 0f;
    protected void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        _animaton = GetComponent<Animator>();
    }
    void Update()
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
            _animaton.SetBool("IsMoving",true);
        }
        else
        {
            nmAgent.isStopped = true; // 탐지 범위를 벗어나면 정지
            _animaton.SetBool("IsMoving",false);
        }
    }

    public abstract void Attack();

    private void TakeDamage(float damage)
    {
        _animaton.SetTrigger("Hit");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _animaton.SetTrigger("Die");
        Destroy(gameObject);
    }
}
