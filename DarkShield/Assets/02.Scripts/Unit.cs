using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IMovable
{
    public float MoveSpeed { get; set; }
    public abstract void TakeDamage(float amount);
    public abstract void Move(Vector3 dir);
}
