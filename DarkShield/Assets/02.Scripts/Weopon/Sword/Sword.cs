public class Sword : Weapon
{
	public override void UseSkill(ActionType skillType)
	{
		if (skills.TryGetValue(skillType, out Skill skill))
		{
			if (skill.skillData.isSkillAimingAtMouse) player.mouseLookDir.LookAt();
			skill.UseSkill();
			player.playerAnimator.SetTrigger(skillType.ToString());
		}
	}
}
