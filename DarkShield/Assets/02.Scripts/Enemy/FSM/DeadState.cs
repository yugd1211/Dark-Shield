using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private Enemy enemy;
    public DeadState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.animotor.SetTrigger("Death");
        GameObject.Destroy(enemy.gameObject, enemy.DestroyTime); // 2초 뒤 적 제거
        enemy.DropCoin();
    }

    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
