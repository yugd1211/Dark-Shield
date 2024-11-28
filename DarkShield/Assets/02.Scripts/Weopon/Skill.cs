using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SOSkill;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private SOSkill _skillData;
    public Element elementType;
    public string skillName;
    public float skillDamage;
    public ParticleSystem skillFX;

    public abstract void UseSkill(Player player);

    protected virtual void Init()
    {
        elementType = _skillData.elementType;
        skillName = _skillData.skillName;
        skillDamage = _skillData.skillDamage;
        skillFX = _skillData.skillFX;
    }
}
