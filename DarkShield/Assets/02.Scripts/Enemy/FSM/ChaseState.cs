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
        enemy.agent.isStopped = false;
        enemy.animotor.SetBool("IsMoving", true);
    }

    public void OnUpdate()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer <= enemy.attackRange)
        {
            enemy.SetState(new AttackState(enemy));
        }
        else if (distanceToPlayer > enemy.detectionRange)
        {
            enemy.SetState(new EnemyIdleState(enemy));
        }
        else
        {
            enemy.Move(enemy.player.position);
        }
    }

    public void OnExit()
    {
        enemy.agent.isStopped = true; // 추적 중지
        enemy.animotor.SetBool("IsMoving", false);
    }
}
