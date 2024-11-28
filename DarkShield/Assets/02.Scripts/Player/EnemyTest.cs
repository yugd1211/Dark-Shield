using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.LogError($"적 남은 체력: {health}");
        print($"적 남은 체력: {health}");
    }
}
