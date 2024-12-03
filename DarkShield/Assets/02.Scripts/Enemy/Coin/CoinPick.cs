using UnityEngine;

public class CoinPick : MonoBehaviour
{
    public int coinValue = 200; // 이 코인의 값

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 플레이어와 충돌하면
        {
            CoinManager.instance.AddCoins(coinValue); // 코인 증가
            Destroy(gameObject); // 코인 오브젝트 제거
        }
    }
}
