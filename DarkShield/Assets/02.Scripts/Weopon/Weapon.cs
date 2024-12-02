using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Dictionary<ActionType, Skill> skills;

    protected virtual void Awake()
    {
        skills = new Dictionary<ActionType, Skill>();
    }

    public abstract void UseSkill(ActionType skillType);

    public void SetSkill(Skill skill)
    {
        if (skills.ContainsKey(skill.actionType))
        {
            Debug.LogError($"�ߺ��� ��ų Ÿ���Դϴ�. {skill.actionType}");
        }
        skills.Add(skill.actionType, skill);
    }
}
