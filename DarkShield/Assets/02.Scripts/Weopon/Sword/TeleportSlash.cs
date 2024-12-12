using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    public float recoveryMoveSpeed;

    private IEnumerator UseTeleportSlash(Collider[] colls, float damage)
    {
        float originSpeed = colls[0].GetComponent<NavMeshAgent>().speed;
        //float originSpeed = 0f;
        //originSpeed = colls[0].GetComponent<IMovable>().MoveSpeed;
        //print($"적의 기존 이동속도 : {colls[0].GetComponent<IMovable>().MoveSpeed}");
        //멈추게하기
        foreach (Collider hitCollider in colls)
        {
            //hitCollider.GetComponent<IMovable>().MoveSpeed = 0f;
            hitCollider.GetComponent<NavMeshAgent>().speed = 0f;
        }
        //print($"적의 바뀐 이동속도 : {colls[0].GetComponent<IMovable>().MoveSpeed}");
        //print($"기다리기 : {recoveryMoveSpeed}초");
        yield return new WaitForSeconds(recoveryMoveSpeed);
        //데미지 주기 && 이동속도 복원
        foreach (Collider hitCollider in colls)
        {
            hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
            print($"입힌 데미지 : {_player.playerStat.GetFinalDamage(damage)}");
            //hitCollider.GetComponent<NavMeshAgent>().isStopped = false;
            hitCollider.GetComponent<NavMeshAgent>().speed = originSpeed;

            //hitCollider.GetComponent<IMovable>().MoveSpeed = originSpeed;
            //print($"스킬이 끝난 적의 이동속도 : {hitCollider.GetComponent<IMovable>().MoveSpeed}");
        }
    }

    public void SkillBoxRange(float damage)
    {
        Collider[] hitColliders = Physics.OverlapBox(_boxCenter, boxSize / 2, _boxCenterPivot.rotation, targetLayer);
        print($"궁극기에 닿은 적의 수 : {hitColliders.Length}");
        if (hitColliders.Length > 0) StartCoroutine(UseTeleportSlash(hitColliders, damage));
        //foreach (Collider hitCollider in hitColliders)
        //{
        //    hitCollider.GetComponent<IDamageable>().TakeDamage(_player.playerStat.GetFinalDamage(damage), false);
        //}
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