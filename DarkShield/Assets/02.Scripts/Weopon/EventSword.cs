using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EventSword : WeaponChange
{
    public float rotateSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime * rotateSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ChangeWeapon(this);
            Destroy(gameObject);
        }
    }
}
