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
        enemy._animotor.SetTrigger("Death");
        GameObject.Destroy(enemy.gameObject, 8f); // 2초 뒤 적 제거
    }

    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
