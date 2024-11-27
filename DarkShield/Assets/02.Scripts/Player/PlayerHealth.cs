using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool Death
    {
        get
        {
            return health <= 0;
        }
    }
    public bool IsHit { get; set; }
    private Player _player;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        //CurState == idleState || CurState == walkState
        if (_player.playerStateMachine.CanEnterHitState())
        {
            IsHit = true;
        }
    }

    private void Init()
    {
        _player = GetComponent<Player>();
    }
}
