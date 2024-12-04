using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSlash : Skill
{
    private Collider _slashArea;
    private List<Collider> _colls;
    private AnimationEventEffects _eventEffects;
    private AnimationEventEffects.EffectInfo _effect;
    private Transform _startPositionRotation;

    public float damage;

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
        /*
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
        */


        // 임시로 에너미가 데미지를 받기위해서 이렇게 수정함
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage, false);
        }


    }

    public override void Init(Player player)
    {
        _slashArea = GetComponent<Collider>();
        _colls = new List<Collider>();
        _eventEffects = player.GetComponent<AnimationEventEffects>();
        _startPositionRotation = _eventEffects.transform;


        damage = skillData.damage;
        actionType = skillData.ActionType;

        _effect = new AnimationEventEffects.EffectInfo(skillData, _startPositionRotation);
    }
}
