using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    public float dashInterval;

    private NavMeshAgent _agent;

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        _agent.speed = moveSpeed;
    }

    public void Move(Vector2 inputDir)
    {
        Vector3 actualMove = new Vector3(inputDir.x, 0, inputDir.y);
        _agent.Move(actualMove.normalized * moveSpeed * Time.deltaTime);
    }

    public void Dash()
    {
        StartCoroutine(DashCoroutine());
    }
    private IEnumerator DashCoroutine()
    {
        _agent.isStopped = true;

        Vector3 dashDirection = transform.forward;
        float elapsedTime = 0f;

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
    public void Spawn(Vector3 position)
    {
        _agent.Warp(position);
    }
}
