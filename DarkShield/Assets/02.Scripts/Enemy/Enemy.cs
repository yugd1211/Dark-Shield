using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Unit
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
    //public RectTransform hpBarForeground; // 초록색 HP

    private IState _currentState;
    public Animator animotor;

    public GameObject coinPrefab;

    public ObjectPool coinpool;

    public float DestroyTime = 8;

    public int coin;

    public float HpAmount
    {
        get
        {
            return health / maxHP;
        }
    }

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animotor = GetComponent<Animator>();
        player = GameManager.Instance.player.transform;
        SetState(new EnemyIdleState(this));

    }

    protected virtual void Update()
    {
        _currentState?.OnUpdate();
    }

    public void SetState(IState newState) //상태변경
    {
        if (_currentState is DeadState)
            return;
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public abstract void Attack();

    public override void Move(Vector3 dir)
    {
        agent.SetDestination(dir);
    }


    public override void TakeDamage(float damage, bool isHit)
    {
        if (_currentState.GetType() == typeof(DeadState) || health <= 0)
            return;
        animotor.SetTrigger("Hit");
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHP);
        //float hpPercent = health / maxHP;
        //hpBarForeground.localScale = new Vector3(hpPercent, 1, 1); // 너비만 조정
        if (health <= 0)
        {
            SetState(new DeadState(this));
            Die();
        }
    }

    public virtual void Die()
    {
        FindObjectOfType<EnemyManager>().RemoveEnemy(this);
    }
    public void DropCoin()
    {
        if (coinpool != null)
        {
            GameObject CoinPick = coinpool.GetObject();
            CoinPick.GetComponent<CoinPick>().coinValue = coin;
            if (CoinPick != null)
            {
                CoinPick.transform.position = transform.position; // 적 위치에 배치
                CoinPick.transform.rotation = transform.rotation;
                CoinPick.SetActive(true);
            }
        }
    }

}
