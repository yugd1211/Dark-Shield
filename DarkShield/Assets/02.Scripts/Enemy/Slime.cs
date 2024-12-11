using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Slime : Enemy
{
	[SerializeField] private float hp = 100f;
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float attackDamage = 10f;
	[SerializeField] private float attackInterval = 1f;
	
	
	private Player _player;
 	private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    public RectTransform hpBarForeground;

    private float attackTimer = 0f;
    
	private void Start()
	{
		_player = FindObjectOfType<Player>();
		_animator = GetComponent<Animator>();
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		Move(_player.transform.position);
		
		hpBarForeground.localScale = new Vector3(HpAmount, 1, 1);
		if (Vector3.Distance(transform.position, _player.transform.position) < attackRange)
		{
			attackTimer += Time.deltaTime;
			if (attackTimer < attackInterval) 
				return;
			_animator.SetBool("Attack", true);
			Attack();
			attackTimer = 0;
		}
		else
			_animator.SetBool("Attack", false);
	}

	public override void TakeDamage(float amount, bool isHit)
	{
		health -= amount;
		_animator.SetBool("Damage", true);
		GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position + (Vector3.up * 2), Quaternion.identity);
		hitEffect.GetComponent<DamagePopup>().SetText(amount.ToString());
		Destroy(hitEffect, 2);
		if (health <= 0)
		{
			health = 0;
			StartCoroutine(DieCoroutine());
		}
	}

	private IEnumerator DieCoroutine()
	{
		_animator.SetBool("Die", true);
		yield return new WaitForSeconds(1f);
		DropCoin();
		Die();
		Destroy(gameObject);
	}

	public override void Attack()
	{
		_player.TakeDamage(attackDamage, false);
	}
	public override void Move(Vector3 dir)
	{
		_navMeshAgent.SetDestination(dir);
	}
	
}
