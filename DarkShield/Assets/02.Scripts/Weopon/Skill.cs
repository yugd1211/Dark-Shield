using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public ActionType actionType;
    public bool isSkillAimingAtMouse;

    public abstract void UseSkill();

    public abstract void Init(Player player);

    public abstract void DamageUpgrade();

    public abstract void SpecialUpgrade();
}

public enum ActionType
{
    Dash,
    Skill1,
    Skill2,
    Skill3
}