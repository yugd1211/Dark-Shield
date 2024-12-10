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
    public GameObject mussangStagePrefab;
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
        return CreateStage(battleStagePrefabs[Random.Range(0, battleStagePrefabs.Length)]);
    }
    
    private GameObject CreateStage(GameObject stage)
    {
        return Instantiate(stage);
    }

    
    public Stage CreateStage()
    {
        int ran = Random.Range(0, 100);
        GameObject newStage;
        if (stageCount == 0)
            newStage = CreateStage(startStagePrefab);
        else if (currentStageIndex >= GameManager.Instance.bossStageIndex)
            newStage = CreateStage(bossStagePrefab);
        else if (GameManager.Instance.isElemental && !GameManager.Instance.isMussang)
        {
            GameManager.Instance.isMussang = true;
            newStage = CreateStage(mussangStagePrefab);
        }
        else if (ran <= 20)
            newStage = CreateStage(shopStagePrefab);
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
        else if (currStage is MussangStage mussang)
            mussang.BattleStart();
        
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
