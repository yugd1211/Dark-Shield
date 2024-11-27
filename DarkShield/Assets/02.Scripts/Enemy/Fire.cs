using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage = 40f; // 기본 데미지

    private void Start()
    {
        Destroy(gameObject,5f); //Test
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("플레이어가 원거리 공격에 맞음!");

            collision.collider.GetComponent<PlayerHealth>().TakeDamage(damage);// 투사체 제거

            Destroy(this.gameObject);
        }
    }
}
