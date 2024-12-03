using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dash : Skill
{
    public float dashSpeed = 10f;
    public float dashInterval = 0.4f;

    private NavMeshAgent _agent;

    private void Awake()
    {
        Init();
    }

    public override void UseSkill()
    {
        StartCoroutine(UseDash());
    }

    private IEnumerator UseDash()
    {
        _agent.isStopped = true;

        Vector3 dashDirection = transform.forward;
        float elapsedTime = 0f;

        //_player.playerAnimator.SetTrigger("Dash");
        while (elapsedTime < dashInterval)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _agent.isStopped = false;
    }

    private void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

}
