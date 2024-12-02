using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Dictionary<ActionType, Skill> skills;
    protected Player player;
    
    
    protected virtual void Awake()
    {
        skills = new Dictionary<ActionType, Skill>();
    }

    public abstract void UseSkill(ActionType skillType);

    public void SetSkill(Skill skill)
    {
        if (skills.ContainsKey(skill.actionType))
        {
            Debug.LogError($"already to {skill.actionType}");
        }
        skills.Add(skill.actionType, skill);
    }
    
    public void Init(Player player)
    {
        this.player = player;
        player.curWeopon = this;
    }
}
