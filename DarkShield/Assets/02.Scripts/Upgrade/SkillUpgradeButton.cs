using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeButton : MonoBehaviour
{
	public enum SkillUpgradeType
	{
		None,
		Damage,
		Special,
	}
	
	public ActionType actionType;
	public SkillUpgradeType skillUpgradeType;
	public UpgradeUI upgradeUI;
	
	private Button _button;
	
	public static bool operator ==(SkillUpgradeButton left, SkillUpgradeButton right)
	{
		if (ReferenceEquals(left, right))
			return true;
		if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
			return false;
		return left.actionType == right.actionType && left.skillUpgradeType == right.skillUpgradeType;
	}

	public static bool operator !=(SkillUpgradeButton left, SkillUpgradeButton right)
	{
		return !(left == right);
	}

	public void Init(UpgradeUI upgradeUI)
	{
		this.upgradeUI = upgradeUI;
		_button = GetComponent<Button>();
		_button.onClick.AddListener(upgradeUI.HideShop);
		_button.onClick.AddListener(Upgrade);
	}

	private void Upgrade()
	{
		if (actionType == ActionType.Dash)
			GameManager.Instance.player.DashUpgrade();
		switch (skillUpgradeType)
		{
			case SkillUpgradeType.Damage:
				GameManager.Instance.player.curWeopon.skills[actionType].DamageUpgrade();
				break;
			case SkillUpgradeType.Special:
				GameManager.Instance.player.curWeopon.skills[actionType].SpecialUpgrade();
				foreach (SkillUpgradeButton button in upgradeUI.buttonPrefabs)
				{
					if (button == this)
					{
						upgradeUI.buttonPrefabs.Remove(button);
						break;
					}
				}
				break;
			case SkillUpgradeType.None:
				break;
		}
	}
}
