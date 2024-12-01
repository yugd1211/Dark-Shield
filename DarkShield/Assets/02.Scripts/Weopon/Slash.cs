using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Skill
{
	public float damage;
	[SerializeField] private ParticleSystem slashFX;
	private Collider _slashArea;
	private List<Collider> _colls;
	private Weapon _weapon;

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		_weapon.SetSkill(this);
	}

	public override void UseSkill()
	{
		StartCoroutine(UseSlash());
	}

	private IEnumerator UseSlash()
	{
		_slashArea.enabled = true;
		ParticleSystem slashInstance = Instantiate(slashFX, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.6f);
		slashInstance.Play();
		yield return new WaitForSeconds(0.5f);
		_slashArea.enabled = false;
		_colls.Clear();

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IDamageable>(out IDamageable other2))
		{
			_colls.Add(other);
			foreach (Collider coll in _colls)
			{
				if (_colls.Contains(coll))
				{
					continue;
				}
			}

			other2.TakeDamage(damage, false);
		}
	}

	private void Init()
	{
		_slashArea = GetComponent<Collider>();
		_colls = new List<Collider>();
		_weapon = GetComponentInParent<Weapon>();
	}
}
