using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
	public float speed;
	public float destroyAfter;
	public float damage;
	private Player _player;
	private Transform _startPositionRotation;

	private void Start()
	{
		Destroy(gameObject, destroyAfter);
	}

	private void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
		{
			damageable.TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
		}
	}

	public void SetDamage(float damage)
	{
		this.damage = damage;
	}

	public void Init(Player player)
	{
		_player = player;
	}
}
