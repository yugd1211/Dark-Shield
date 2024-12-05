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

    public int currentStageIndex = 0;
    
    public void Init()
    {
        player = FindObjectOfType<Player>();
        
        currStage = CreateStage();
        while (currStage.portalPoints.Count > Random.Range(0, 3))
            currStage.CreateNextPortal();
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
        if (currentStageIndex == 0)
            newStage = CreateStartStage();
        else if (ran <= 10)
            newStage = CreateShopStage();
        else 
            newStage = CreateBattleStage();
        newStage.transform.position = new Vector3(0, 0, currentStageIndex * 100);
        newStage.transform.SetParent(transform);
        
        Stage stage = newStage.GetComponent<Stage>();
        stages.Add(stage);
        currentStageIndex++;
        stage.Init(this);
        return stage;
    }

    public void ChangeStage(Stage stage)
    {
        currStage = stage;
        currStage.StartStage();
        if (currStage is BattleStage battle)
            battle.StartBattle();
        
        for (int i = 0; i < currStage.portalPoints.Count; i++)
            currStage.CreateNextPortal();
    }
}
