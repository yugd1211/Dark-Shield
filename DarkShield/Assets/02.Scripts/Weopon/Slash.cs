using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float damage;
    [SerializeField] private ParticleSystem slashFX;
    private Collider _slashArea;

    private void Awake()
    {
        Init();
    }

    public void UseSkill()
    {
        StartCoroutine(UseSlash());
    }

    private IEnumerator UseSlash()
    {
        _slashArea.enabled = true;
        ParticleSystem slashInstance = Instantiate(slashFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        slashInstance.Play();
        yield return new WaitForSeconds(0.5f);
        _slashArea.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Enemy>().TakeDamage(damage);
    }

    private void Init()
    {
        _slashArea = GetComponent<Collider>();
    }
}
