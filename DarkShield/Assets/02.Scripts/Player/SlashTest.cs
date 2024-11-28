using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTest : MonoBehaviour
{
    public float damage;
    public ParticleSystem slashFX;
    public Collider slashArea;

    private void Awake()
    {
        slashArea = GetComponent<Collider>();
    }

    public void UseSkill()
    {
        StartCoroutine(UseSlash());
    }

    private IEnumerator UseSlash()
    {
        slashArea.enabled = true;
        ParticleSystem slashInstance = Instantiate(slashFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        slashInstance.Play();
        yield return new WaitForSeconds(0.5f);
        slashArea.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyTest>().TakeDamage(damage);
    }
}
