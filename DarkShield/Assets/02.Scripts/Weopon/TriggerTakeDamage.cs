using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTakeDamage : MonoBehaviour
{
    public float damage;
    private List<Collider> _colls;

    private void Awake()
    {
        _colls = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _colls.Add(other);
        foreach (Collider coll in _colls)
        {
            if (_colls.Contains(coll))
            {
                continue;
            }
        }
        //중복 검사 해야 함.
        other.GetComponent<EnemyTest>().TakeDamage(damage);
        _colls.Clear();
    }
}
