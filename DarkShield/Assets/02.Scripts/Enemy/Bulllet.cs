using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{
    public float damage = 40f; // 기본 데미지
    private ObjectPool BulletPool;
    private Vector3 direction;
    private bool isLaunched = false;
    public float Speed = 10f;
    private void Awake()
    {
        BulletPool = FindObjectOfType<ObjectPool>();
    }

    public void Launch(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized; // 방향 계산
        isLaunched = true; // 발사 상태 활성화
    }

    private void Update()
    {
        if (isLaunched)
        {
            transform.position += direction * Speed * Time.deltaTime;
        }
    }


    private void OnEnable()
    {
        // 5초 후에 비활성화 (풀로 반환)
        Invoke("Deactivate", 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("플레이어가 원거리 공격에 맞음!");

            collision.collider.GetComponent<PlayerHealth>().TakeDamage(damage);// 투사체 제거
        }
        Deactivate();
    }

    /*private void Deactivate()
    {
        CancelInvoke(); // 예약된 호출 취소
        BulletPool.ReturnObject(gameObject); // 오브젝트를 풀로 반환
    }*/
    private void Deactivate()
    {
        isLaunched = false; // 이동 중지
        gameObject.SetActive(false); // 오브젝트 풀로 반환
    }
}
