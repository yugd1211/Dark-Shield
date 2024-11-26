using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    public float dashInterval;

    private Player _player;
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
        _agent.isStopped = true; // NavMeshAgent 동작 중지

        Vector3 dashDirection = transform.forward; // 현재 보는 방향으로 대시
        float elapsedTime = 0f;

        while (elapsedTime < dashInterval)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _agent.isStopped = false; // NavMeshAgent 다시 활성화
    }

    private void Init()
    {
        _player = GetComponent<Player>();
        _agent = GetComponent<NavMeshAgent>();
    }
}
