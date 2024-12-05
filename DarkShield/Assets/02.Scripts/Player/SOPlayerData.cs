using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/SOPlayerData")]
public class SOPlayerData : ScriptableObject
{
    public float health;
    public float damage;
    public float criticalChance;
    public float criticalDamage;
}
