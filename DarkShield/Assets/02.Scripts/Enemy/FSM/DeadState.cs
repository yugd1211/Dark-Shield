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
        enemy.gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        // Collider 비활성화
        Collider[] colliders = enemy.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
        }
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
