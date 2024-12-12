using System.Collections;
using UnityEngine;

public class TeleportSlash : Skill
{
    private AnimationEventEffects _eventEffects;
    private AnimationEventEffects.EffectInfo _effect;
    //궁극기 해보기 위한 변수
    public LayerMask targetLayer;
    public Transform _teleportSlashPivot;
    public SOSkill teleportSlash1;

    //====================================
    //오버랩 구현에 필요한 변수
    //====================================
    public Vector3 boxSize;
    public Vector3 boxOffset;

    private Vector3 _boxCenter;
    private Transform _boxCenterPivot;
    private Player _player;

    private void Update()
    {
        _boxCenter = _boxCenterPivot.TransformPoint(boxOffset);
    }

    private void OnDrawGizmos()
    {
        if (_boxCenterPivot == null) return;

        Gizmos.color = Color.cyan;

        // 박스 기즈모
        _boxCenter = _boxCenterPivot.TransformPoint(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(_boxCenter, _boxCenterPivot.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
        Gizmos.color = new Color(1, 1, 1, 0.3f);
        Gizmos.DrawCube(Vector3.zero, boxSize);
    }

    public override void UseSkill()
    {
        _eventEffects.SetEffects(_effect);
    }

    private IEnumerator UseTeleportSlash()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void SkillBoxRange(float damage)
    {
        Collider[] hitColliders = Physics.OverlapBox(_boxCenter, boxSize / 2, _boxCenterPivot.rotation, targetLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("감지된 이름 " + hitCollider.name);
            print("가한 데미지 : " + damage);
            hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
        }
    }

    public override void Init(Player player)
    {
        //여기서부터 궁극기 구현
        _player = player;
        _eventEffects = player.GetComponent<AnimationEventEffects>();
        _teleportSlashPivot = GameObject.Find("TeleportSlashPivot").transform;
        teleportSlash1.startPositionRotation = _teleportSlashPivot;
        _effect = new AnimationEventEffects.EffectInfo(teleportSlash1, SkillBoxRange);
        _boxCenterPivot = _teleportSlashPivot;
    }

    public override void DamageUpgrade()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialUpgrade()
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeEffect(ElementChange element)
    {
        throw new System.NotImplementedException();
    }
}