using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Enemy enemy;

    public AttackState(Enemy enemy)
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
        if(distanceToPlayer > enemy.attackRange)
        {
            enemy.SetState(new ChaseState(enemy));
        }
        else if(Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            enemy.Attack();
        }
    }

    public void OnExit()
    {

    }
}
