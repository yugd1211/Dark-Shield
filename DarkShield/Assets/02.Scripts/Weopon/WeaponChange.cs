using UnityEditor.Animations;
using UnityEngine;

public class WeaponChange : MonoBehaviour, IInteractable
{
    public AnimatorController animController;
    public GameObject weaponPrefab;
    public float rotateSpeed;
    public bool CanInteract() => !GameManager.Instance.player.isEquip;
    
    public void Interact(Interactor player)
    {
        player.GetComponent<Player>().ChangeWeapon(this);
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime * rotateSpeed);
    }
}
