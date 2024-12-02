using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float dashSpeed;
    public float rotateSpeed;
    public float dashInterval;

    private NavMeshAgent _agent;
    private Player _player;

    private void Awake()
    {
        Init();
    }

    public void Rotate(Vector2 inputDir)
    {
        if (inputDir.sqrMagnitude < 0.1f) return;

        Vector3 direction = new Vector3(inputDir.x, 0, inputDir.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
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
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Spawn(Vector3 position)
    {
        _agent.Warp(position);
    }
    public void Move(Vector3 actualMove)
    {
        _agent.Move(actualMove * Time.deltaTime);

    }
}
