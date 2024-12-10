using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public Dictionary<ActionType, Skill> skills;
	protected Player player;
	public float damage;

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

	public void UpgradeSkill(ActionType skillType)
	{
		if (skills.ContainsKey(skillType))
		{
			// skills[skillType].Upgrade();
		}
	}

	public void ChangeElement(ElementChange element)
	{
		foreach (ActionType actionType in element.actionType)
		{
			if (skills.TryGetValue(actionType, out Skill skill))
			{
				skill.ChangeEffect(element);
			}
		}
	}
}
