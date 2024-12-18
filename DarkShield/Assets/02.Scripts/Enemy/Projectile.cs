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

    public void Init(ObjectPool objectPool)
    {
        ProjectilePool = objectPool;
    }

    public void Launch(Vector3 targetPosition)
    {
        //direction = (targetPosition - transform.position).normalized; // 방향 계산
        isLaunched = true; // 발사 상태 활성화

    }

    private void Update()
    {

        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    public string target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(target))
        {
            other.GetComponent<Unit>().TakeDamage(damage, true);// 투사체 제거
            Deactivate();
        }
    }


    private void Deactivate()
    {
        isLaunched = false; // 이동 중지
        ProjectilePool.ReturnObject(gameObject); // 오브젝트 풀로 반환
    }
}
