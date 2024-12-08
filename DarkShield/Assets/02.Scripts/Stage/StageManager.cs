using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    public Player player;
    
    public List<Stage> stages = new List<Stage>();
    
    public GameObject[] battleStagePrefabs;
    public GameObject shopStagePrefab;
    public GameObject bossStagePrefab;
    public GameObject startStagePrefab;
    public Stage currStage;

    public int stageCount = 0;
    public int currentStageIndex = 0;
    
    public void Init()
    {
        player = FindObjectOfType<Player>();
        
        currStage = CreateStage();
    }

    private GameObject CreateBattleStage()
    {
        return Instantiate(battleStagePrefabs[Random.Range(0, battleStagePrefabs.Length)]);
    }

    private GameObject CreateShopStage()
    {
        return Instantiate(shopStagePrefab);
    }
    
    private GameObject CreateBossStage()
    {
        return Instantiate(bossStagePrefab);
    }
    
    private GameObject CreateStartStage()
    {
        return Instantiate(startStagePrefab);
    }
    
    public Stage CreateStage()
    {
        int ran = Random.Range(0, 100);
        GameObject newStage;
        if (stageCount == 0)
            newStage = CreateStartStage();
        else if (currentStageIndex >= GameManager.Instance.bossStageIndex)
            newStage = CreateBossStage();
        else if (ran <= 30)
            newStage = CreateShopStage();
        else 
            newStage = CreateBattleStage();
        newStage.transform.position = new Vector3(0, 0, stageCount * 100);
        newStage.transform.SetParent(transform);
        
        Stage stage = newStage.GetComponent<Stage>();
        stages.Add(stage);
        stageCount++;
        stage.Init(this);
        return stage;
    }

    public void ChangeStage(Stage stage)
    {
        currStage = stage;
        currStage.GoToStage();
        currentStageIndex++;
        if (currStage is BattleStage battle)
            battle.BattleStart();
        else if (currStage is BossStage boss)
        {
            boss.BattleStart();
            return;
        }
        
        if (currentStageIndex >= GameManager.Instance.bossStageIndex)
        {
            currStage.CreateNextPortal();   
        }
        else
        {
            for (int i = 0; i < currStage.portalPoints.Count; i++)
                currStage.CreateNextPortal();
        }
    }
}
