using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeButton : MonoBehaviour
{
	public enum SkillUpgradeType
	{
		Damage,
		Special,
	}
	public ActionType actionType;
	public SkillUpgradeType skillUpgradeType;
	
	
	private Button _button;
	private void Start()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(Upgrade);
	}

	public void Upgrade()
	{
		if (actionType == ActionType.Dash)
		{
			GameManager.Instance.player.DashUpgrade();
			return;
		}
		switch (skillUpgradeType)
		{
			case SkillUpgradeType.Damage:
				// GameManager.Instance.player.curWeopon.skills[actionType].DamageUpgrade();
				break;
			case SkillUpgradeType.Special:
				// GameManager.Instance.player.curWeopon.skills[actionType].SpecialUpgrade();
				break;
		}
	}
}
