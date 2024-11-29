using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSlash : MonoBehaviour
{
    public float damage;
    [SerializeField] private Collider _teleportSlashArea;

    private void Awake()
    {
        Init();
    }

    public void UseSkill()
    {
        StartCoroutine(UseTeleportSlash());
    }

    private IEnumerator UseTeleportSlash()
    {
        yield return new WaitForSeconds(1.1f);
        _teleportSlashArea.enabled = true;
        yield return new WaitForSeconds(2f);
        _teleportSlashArea.enabled = false;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    other.GetComponent<EnemyTest>().TakeDamage(damage);
    //}

    private void Init()
    {
        _teleportSlashArea = GameObject.Find("TeleportSlashCollider").GetComponent<Collider>();
        _teleportSlashArea.GetComponent<TriggerTakeDamage>().damage = damage;
    }
}
