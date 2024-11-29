using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTakeDamage : MonoBehaviour
{
    public float damage;
    public List<Collider> colls;

    private void Awake()
    {
        colls = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable other2))
        {
            colls.Add(other);
            foreach (Collider coll in colls)
            {
                if (colls.Contains(coll))
                {
                    continue;
                }
            }
            //중복 검사 해야 함.
            other2.TakeDamage(damage);
        }
    }
}
