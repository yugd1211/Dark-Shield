using UnityEngine;

public abstract class Skill : MonoBehaviour
{
	public ActionType actionType;
	public SOSkill skillData;
	public float damage;

	public abstract void UseSkill();

	public abstract void Init(Player player);
}

public enum ActionType
{
	Dash,
	Skill1,
	Skill2,
	Skill3
}
