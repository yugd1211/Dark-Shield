using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDleState : IState
{
    private Enemy enemy;

    public IDleState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.Agent.isStopped = true;
        enemy.animator.SetBool("IsMoving", false);

    }
    public void OnUpdate()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer <= enemy.detectionRange)
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public void OnExit()
    {

    }
}
