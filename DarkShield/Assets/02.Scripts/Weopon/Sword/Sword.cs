public class Sword : Weapon
{
    public override void UseSkill(ActionType skillType)
    {
        if (skills.TryGetValue(skillType, out Skill skill))
        {
            if (skill.skillData.isSkillAimingAtMouse) player.mouseLookDir.LookAt();
            player.playerAnimator.SetTrigger(skillType.ToString());
            skill.UseSkill();
        }
    }

    public override void UseSkill(ActionType skillType, int comboCnt)
    {
        if (skills.TryGetValue(skillType, out Skill skill))
        {
            if (skill.skillData.isSkillAimingAtMouse) player.mouseLookDir.LookAt();
            player.playerAnimator.SetTrigger(skillType.ToString());
            player.playerAnimator.SetInteger("Skill1Combo", comboCnt);
            skill.UseSkill();
        }
    }
}
