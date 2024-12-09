using UnityEngine;

public class ElementChange : MonoBehaviour, IInteractable
{
    public SOSkill[] skillData;
    public ActionType actionType;
    
    public void Interact(Interactor player)
    {
        if (player.TryGetComponent(out Player p))
            p.ChangeElement(this);
        Destroy(gameObject);
    }
    public bool CanInteract()
    {
        return true;
    }
}
