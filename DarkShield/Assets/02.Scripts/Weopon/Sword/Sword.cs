public class Sword : Weapon
{
	public override void UseSkill(ActionType skillType)
	{
		if (skills.TryGetValue(skillType, out Skill skill))
		{
			player.playerAnimator.SetTrigger(skillType.ToString());
			skill.UseSkill();
		}
	}
}
