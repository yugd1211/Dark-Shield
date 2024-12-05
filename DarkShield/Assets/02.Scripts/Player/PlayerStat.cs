using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private Player _player;
    [SerializeField] private SOPlayerData _playerData;

    //PlayerData
    private float playerDamage;
    private float criticalChance;
    private float criticalDamage;

    //WeaponData
    private float weaponDamage => _player.curWeopon.damage;

    private void Awake()
    {
        Init();
    }

    public float GetFinalDamage(Skill skill)
    {
        float totalDamage = playerDamage + weaponDamage + skill.damage;
        totalDamage = Critical(totalDamage); //크리티컬 판단 //true 이면 크뎀 적용, 아니면 총합한 데미지
        return totalDamage;
    }

    private float Critical(float damage)
    {
        float _finalDamage;
        float random = Random.Range(0f, 100f);
        if (criticalChance >= random)
            _finalDamage = damage * (1 + criticalDamage);
        else
            _finalDamage = damage;
        return _finalDamage;
    }

    //크리티컬 데미지 세팅(업그레이드 할 때 사용 할 연산)
    public void SetCriticalDamage(float damage)
    {
        criticalDamage += damage / 100;
    }
    #region Init
    private void Init()
    {
        _player = GetComponent<Player>();
        SetPlayerData();
    }

    private void SetPlayerData()
    {
        playerDamage = _playerData.damage;
        criticalChance = _playerData.criticalChance;
        criticalDamage = _playerData.criticalDamage / 100;
    }
    #endregion
}
