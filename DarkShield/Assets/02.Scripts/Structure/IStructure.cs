using UnityEngine;

public interface IStructure
{
	protected float EffectAmount { get; set; }
	protected void Affect(GameObject target /*GameObject -> Unit or affected*/) ;
	protected void OnTargetEnter(GameObject target);
	protected void OnTargetExit(GameObject target);
}
