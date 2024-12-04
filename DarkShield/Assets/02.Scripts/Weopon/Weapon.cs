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

    public abstract void UseSkill(ActionType skillType);

    public void SetSkill(Skill skill)
    {
        if (skills.ContainsKey(skill.actionType))
        {
            Debug.LogError($"already to {skill.actionType}");
        }
        skills.Add(skill.actionType, skill);
    }

    private void FindSkill()
    {
        Skill[] findSkills = GetComponentsInChildren<Skill>();
        foreach (Skill skill in findSkills)
        {
            skill.Init(player);
            SetSkill(skill);
        }
    }

    public void Init(Player player)
    {
        this.player = player;
        player.curWeopon = this;

        skills = new Dictionary<ActionType, Skill>();

        FindSkill();
    }
}
