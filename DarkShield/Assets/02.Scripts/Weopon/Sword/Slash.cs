using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Slash : Skill
{
	private Collider _slashArea;
	private List<Collider> _colls;
	private Weapon _weapon;
	private AnimationEventEffects _eventEffects;
	private AnimationEventEffects.EffectInfo _effect;
	private Transform _startPositionRotation;
	[SerializeField] private SOSkill _skillData;

	public float damage;

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		_weapon.SetSkill(this);
		_effect = new AnimationEventEffects.EffectInfo(_skillData, _startPositionRotation);
	}

	public override void UseSkill()
	{
		_eventEffects.SetEffects(_effect);
		StartCoroutine(UseSlash());
	}

	private IEnumerator UseSlash()
	{
		yield return new WaitForSeconds(0.1f);
		_slashArea.enabled = true;
		yield return new WaitForSeconds(0.3f);
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
		_eventEffects = GameObject.Find("Player").GetComponent<AnimationEventEffects>();
		_startPositionRotation = _eventEffects.transform;

		damage = _skillData.damage;
		actionType = _skillData.ActionType;
	}
}
