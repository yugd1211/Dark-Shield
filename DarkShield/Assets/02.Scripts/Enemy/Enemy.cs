using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    public NavMeshAgent agent; // NavMeshAgent
    public float detectionRange = 15f; // 플레이어 탐지 범위
    public float attackCooldown = 2f; // 공격 쿨타임

    public float attackRange = 1f;

    public float lastAttackTime = 0f;

    public float health = 200; //현재 채력
    public float AttackPower = 20f;//공격력

    public float maxHP = 200f;
    public RectTransform hpBarForeground; // 초록색 HP 

    private IState _currentState;
    public Animator animotor;

    public GameObject coinPrefab;
    public bool hasDroppedCoin = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animotor = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        SetState(new EnemyIdleState(this));

    }

    protected virtual void Update()
    {
        _currentState?.OnUpdate();
    }

    public void SetState(IState newState) //상태변경
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public abstract void Attack();
    public void TakeDamage(float damage)
    {
        animotor.SetTrigger("Hit");
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHP);
        float hpPercent = health / maxHP;
        hpBarForeground.localScale = new Vector3(hpPercent, 1, 1); // 너비만 조정
        if (health <= 0)
        {
            SetState(new DeadState(this));
        }
    }

    public void Die()
    {
        animotor.SetTrigger("Death");
        FindObjectOfType<EnemyManager>().RemoveEnemy(this);
        Destroy(gameObject, 5f);
    }
}
