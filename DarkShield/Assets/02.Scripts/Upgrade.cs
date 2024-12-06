using UnityEngine;

public class Upgrade : MonoBehaviour
{
	
	// 업그레이드 패널에서 관련 함수들
	public void DashUpgrade()	
	{
		GameManager.Instance.player.DashUpgrade();
	}
	
	public void WeaponSkill1SpecialUpgrade()
	{
		// GameManager.Instance.player.weapon.skills[0].SpecialUpgrade()
		Debug.Log("Weapon Skill 1 Special Upgrade");
	}
	
	public void WeaponSkill2SpecialUpgrade()
	{
		Debug.Log("Weapon Skill 2 Special Upgrade");
	}
	
	public void WeaponSkill1DamageUpgrade()
	{
		Debug.Log("Weapon Skill 1 Damage Upgrade");
	}
	
	public void WeaponSkill2DamageUpgrade()
	{
		Debug.Log("Weapon Skill 2 Damage Upgrade");
	}
	
	// 상점에서의 강화 함수들
	
}
