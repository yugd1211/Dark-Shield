using UnityEngine;

public interface IMovable
{
    public float MoveSpeed { get; set; }
    public void Move(Vector3 dir);
}
