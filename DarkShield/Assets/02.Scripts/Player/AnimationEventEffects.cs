using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public EffectInfo(SOSkill skillData, Transform trasform)
		{
			Effect = skillData.skillEffect;
			StartPositionRotation = trasform;
			DestroyAfter = skillData.destroyAfter;
			UseLocalPosition = skillData.useLocalPosition;
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

		var instance = Instantiate(effects[EffectNumber].Effect, effects[EffectNumber].StartPositionRotation.position, effects[EffectNumber].StartPositionRotation.rotation);

		if (effects[EffectNumber].UseLocalPosition)
		{
			instance.transform.parent = effects[EffectNumber].StartPositionRotation.transform;
			instance.transform.localPosition = Vector3.zero;
			instance.transform.localRotation = new Quaternion();
		}
		Destroy(instance, effects[EffectNumber].DestroyAfter);
	}

	public void EndEffect()
	{
		effects.Clear();
	}
}
