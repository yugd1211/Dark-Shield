using UnityEngine;

public interface IStructure
{
	public float EffectAmount { get; set; }
	public void Affect(IMovable target);
	public void OnTargetEnter(IMovable target);
	public void OnTargetExit(IMovable target);
}
