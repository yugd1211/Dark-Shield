using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour, IStructure
{
    public float damage;
    public float damageInterval;
    private Dictionary<Unit, float> _targets = new Dictionary<Unit, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit target))
            OnTargetEnter(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit target))
            OnTargetExit(target);
    }

    private void Update()
    {
        Dictionary<Unit, float> tmp = new Dictionary<Unit, float>(_targets);
        foreach (KeyValuePair<Unit, float> target in tmp)
            Affect(target.Key);
    }

    public void Affect(Unit target)
    {
        if (_targets[target] >= damageInterval)
        {
            target.TakeDamage(damage, false);
            _targets[target] = 0;
        }
        else
            _targets[target] += Time.deltaTime;
    }

    public void OnTargetEnter(Unit target)
    {
        _targets[target] = 0;
    }

    public void OnTargetExit(Unit target)
    {
        _targets.Remove(target);
    }
}
