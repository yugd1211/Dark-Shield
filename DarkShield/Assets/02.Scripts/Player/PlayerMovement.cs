using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float rotateSpeed;

    private NavMeshAgent _agent;

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

    public void Move(Vector3 actualMove)
    {
        _agent.Move(actualMove * Time.deltaTime);

    }

    public void Spawn(Vector3 position)
    {
        _agent.Warp(position);
    }

    private void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
}
