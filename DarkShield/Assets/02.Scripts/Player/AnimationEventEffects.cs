using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventEffects : MonoBehaviour
{
    public List<EffectInfo> effects;

    [System.Serializable]
    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
        public UnityAction<float> targetScan;
        public float damage;
        public float damagePercent;

        public EffectInfo(SOSkill skillData, UnityAction<float> action)
        {
            Effect = skillData.skillEffect;
            StartPositionRotation = skillData.startPositionRotation;
            DestroyAfter = skillData.destroyAfter;
            UseLocalPosition = skillData.useLocalPosition;
            targetScan = action;
            damage = skillData.damage;
            damagePercent = skillData.damagePercent;
        }
    }

    public void SetEffects(EffectInfo effectInfo)
    {
        effects.Add(effectInfo);
    }

    public void InstantiateEffect(int EffectNumber)
    {
        if (effects == null || effects.Count <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        //print("이펙트 프리팹 생성됨");
        var instance = Instantiate(effects[EffectNumber].Effect, effects[EffectNumber].StartPositionRotation.position, effects[EffectNumber].StartPositionRotation.rotation);

        if (effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        Destroy(instance, effects[EffectNumber].DestroyAfter);
    }

    public void TargetScan(int ActionNumber)
    {
        effects[ActionNumber].targetScan?.Invoke(effects[ActionNumber].damage);
    }

    public void EndEffect()
    {
        //print("이펙트 프리팹 지워짐");
        effects.Clear();
    }
}
