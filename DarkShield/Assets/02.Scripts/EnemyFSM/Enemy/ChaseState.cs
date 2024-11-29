using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private Enemy enemy;
    public ChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.Agent.isStopped = false;
        enemy.animator.SetBool("IsMoving", true);
    }
    public void OnUpdate()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer <= enemy.attackRange)
        {
            //enemy.ChangeState(new AttackState(enemy));
        }
        else if (distanceToPlayer > enemy.detectionRange)
        {
            //enemy.ChangeState(new IdleState(enemy));
        }
        else
        {
            enemy.Agent.SetDestination(enemy.player.position);
        }

    }

    public void OnExit()
    {
        enemy.Agent.isStopped = true;
        enemy.animator.SetBool("IsMoving", false);
    }
}
