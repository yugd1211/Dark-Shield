using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public float damage = 40f; // 기본 데미지
    private ObjectPool ProjectilePool;
    private Vector3 direction;
    private bool isLaunched = false;
    public float Speed = 10f;
    private void Awake()
    {
        ProjectilePool = FindObjectOfType<ObjectPool>();
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
        Invoke("Deactivate", 5f); // 일정 시간이 지나면 비활성화
    }

    public string target;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable unit))
        {
            unit.TakeDamage(damage, true);
        }

        Deactivate();
    }

  
    private void Deactivate()
    {
        isLaunched = false; // 이동 중지
        gameObject.SetActive(false); // 오브젝트 풀로 반환
    }
}
