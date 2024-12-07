using UnityEngine;
public class Gold
{
	public int Amount { get; private set; }
	
	public Gold(int amount)
	{
		Amount = amount;
	}
	
	public void AddGold(int amount)
	{
		Amount += amount;
		// Debug.Log(Amount);
	}
	
	public void SubGold(int amount)
	{
		if (amount > Amount)
			Amount = 0;
		else
			Amount -= amount;
	}
}
