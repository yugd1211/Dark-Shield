using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SOSkill")]
public class SOSkill : ScriptableObject
{
    public float damage;
    public float damagePercent;
    public GameObject skillEffect;
    public float destroyAfter = 10f;
    public bool useLocalPosition = false;
    public Transform startPositionRotation;
}
