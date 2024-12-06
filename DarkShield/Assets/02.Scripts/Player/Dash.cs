using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dash : Skill
{
    public float dashSpeed = 10f;
    public float dashInterval = 0.4f;
    public int maxDashCount;
    public float maxDashCooltime = 1f;

    private float _curDashCooltime = 0f;
    private int _curDashCount;

    private NavMeshAgent _agent;
    //추후에 Dash 이펙트에 사용할 변수들
    //private AnimationEventEffects _eventEffects;
    //private AnimationEventEffects.EffectInfo _effect;
    //private Transform _startPositionRotation;

    private void Awake()
    {
        Init();
    }

    public override void UseSkill()
    {
        //_eventEffects.SetEffects(_effect);
        StartCoroutine(UseDash());
    }

    private IEnumerator UseDash()
    {
        _curDashCount++;
        _agent.isStopped = true;

        Vector3 dashDirection = transform.forward;
        float elapsedTime = 0f;

        while (elapsedTime < dashInterval)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _agent.isStopped = false;
        if (!CanDash()) StartCoroutine(DashCooltime());
    }

    //쿨타임이 도는 메서드
    private IEnumerator DashCooltime()
    {
        //현재 쿨타임이 max쿨타임보다 작을때까지
        while (_curDashCooltime <= maxDashCooltime)
        {
            _curDashCooltime += Time.deltaTime;
            yield return null;
        }

        _curDashCount = 0;
        _curDashCooltime = 0f;
    }

    public bool CanDash()
    {
        if (_curDashCount < maxDashCount) return true;
        else return false;
    }

    public override void Init(Player player)
    {
    }

    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
        //Dash 이펙트를 사용하기 위한 초기화 해야 할 변수들
        //_eventEffects = GetComponent<AnimationEventEffects>();
        //_startPositionRotation = transform;
        //_effect = new AnimationEventEffects.EffectInfo(skillData, transform);
    }

    public override void DamageUpgrade()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
