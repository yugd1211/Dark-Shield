using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSlash : MonoBehaviour
{
    public float damage;
    private Collider _teleportSlashArea;

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
        _teleportSlashArea.GetComponent<TriggerTakeDamage>().colls.Clear();
    }

    private void Init()
    {
        _teleportSlashArea = GameObject.Find("TeleportSlashCollider").GetComponent<Collider>();
        _teleportSlashArea.GetComponent<TriggerTakeDamage>().damage = damage;
    }
}
