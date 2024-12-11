using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // 풀링할 오브젝트의 프리팹
    public int poolSize = 10; // 초기 풀 크기

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        // 초기 풀 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // 오브젝트 가져오기
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // 풀이 비어있으면 새로 생성
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // 오브젝트 반환
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
