using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private Enemy enemy;
    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.agent.isStopped = true;
        enemy.animotor.SetBool("IsMoving", false);         
    }

    public void OnUpdate()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer <= enemy.detectionRange)
        {
            enemy.SetState(new ChaseState(enemy));
        }
    }

    public void OnExit()
    {

    }
}
