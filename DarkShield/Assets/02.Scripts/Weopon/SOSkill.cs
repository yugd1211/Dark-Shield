using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSkill", menuName = "ScriptableObject/Skill")]
public class SOSkill : ScriptableObject
{
    public enum Element
    {
        None, FIRE, ELEC, WATER, LIGHT, DARK
    }

    public Element elementType;
    public string skillName;
    public float skillDamage;
    //스킬 업그레이드 가능한 변수
    //public int level;
    //public float skillDamage[];
    public ParticleSystem skillFX;
}
