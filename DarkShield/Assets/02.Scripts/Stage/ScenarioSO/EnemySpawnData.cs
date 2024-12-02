using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemySpawnInfo
{
    public string enemyName;
    public Enemy enemyPrefab;
    public int count;
}

[System.Serializable]
public struct EnemySpawnWave
{
    public List<EnemySpawnInfo> enemies;
    // 에너미가 한번에 생성되는게 아니라 순서대로 생성되는 방식으로 하기 위해 시간 간격을 두고 생성할 예정
    public float spawnFrequency;
}

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "ScriptableObjects/EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    public List<EnemySpawnWave> waves;
}