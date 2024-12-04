using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSlash : Skill
{
    private AnimationEventEffects _eventEffects;
    private AnimationEventEffects.EffectInfo _effect;
    private Transform _startPositionRotation;

    public override void UseSkill()
    {
        _eventEffects.SetEffects(_effect);
    }


    public override void Init(Player player)
    {
        _eventEffects = player.GetComponent<AnimationEventEffects>();
        _startPositionRotation = _eventEffects.transform;
        _effect = new AnimationEventEffects.EffectInfo(skillData, _startPositionRotation);

        damage = skillData.damage;
        actionType = skillData.ActionType;
    }
}
