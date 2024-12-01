using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [HideInInspector] public ActionType actionType;
    public abstract void UseSkill();
}

public enum ActionType
{
    Dash,
    Skill1,
    Skill2,
    Skill3
}
