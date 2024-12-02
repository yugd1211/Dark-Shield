using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
   public static CoinManager instance;
   public int totalCoins = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        totalCoins = 0;
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        print($"현재 코인은 {totalCoins} 입니다");
    }
}
