using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private Enemy enemy;

    private bool isDead = false;
    public DeadState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        if (isDead) return;
        isDead = true;
        enemy.animotor.SetTrigger("Death");
        GameObject.Destroy(enemy.gameObject, 8f); // 2초 뒤 적 제거
        if (enemy.coinPrefab != null && !enemy.hasDroppedCoin)
        {

            GameObject.Instantiate(enemy.coinPrefab, enemy.transform.position, Quaternion.identity);
            enemy.hasDroppedCoin = true;
        }
    }

    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
