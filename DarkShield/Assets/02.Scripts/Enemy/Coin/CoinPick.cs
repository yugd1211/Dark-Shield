using UnityEngine;

public class CoinPick : MonoBehaviour
{
    public int coinValue = 200; // 이 코인의 값

    private ObjectPool coinPool;

    private void Start()
    {
        coinPool = GetComponent<ObjectPool>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 플레이어와 충돌하면
        {
            GameManager.Instance.gold.AddGold(coinValue); // 플레이어의 골드에 코인의 값만큼 추가
            if(coinPool != null)
            {
                coinPool.ReturnObject(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
