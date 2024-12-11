using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MultiLinkedListNode<T>
{
    public T Value { get; set; }
    public MultiLinkedListNode<T> Prev { get; set; }
    public List<MultiLinkedListNode<T>> Next { get; set; }

    public MultiLinkedListNode(T value)
    {
        Value = value;
        Prev = null;
        Next = new List<MultiLinkedListNode<T>>();
    }
}
public class MultiLinkedList<T>
{
    public MultiLinkedListNode<T> Current { get; private set; }
    
    public void Add(T value)
    {
        MultiLinkedListNode<T> newNode = new MultiLinkedListNode<T>(value);
        if (Current == null)
            Current = newNode;
        else
        {
            Current.Next.Add(newNode);
            newNode.Prev = Current;
        }
    }

    public bool Move(T value)
    {
        foreach (MultiLinkedListNode<T> node in Current.Next)
        {
            if (!node.Value.Equals(value)) 
                continue;
            Current = node;
            return true;
        }
        return false;
    }
}


public class StageManager : MonoBehaviour
{
    public Player player;

    public List<Stage> stages = new List<Stage>();
    public MultiLinkedList<Stage> stageList = new MultiLinkedList<Stage>();
    
    public GameObject[] battleStagePrefabs;
    public GameObject shopStagePrefab;
    public GameObject bossStagePrefab;
    public GameObject startStagePrefab;
    public GameObject mussangStagePrefab;
    public Stage currStage;

    public int stageCount = 0;
    public int currentStageDepth = 0;
    
    public void Init()
    {
        player = FindObjectOfType<Player>();
        currStage = CreateStage();
        stageList.Add(currStage);
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
        else if (currentStageDepth >= GameManager.Instance.bossStageIndex)
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
        stageList.Add(stage);
        stageCount++;
        stage.Init(this);
        return stage;
    }

    public void ChangeStage(Stage stage)
    {
        currStage = stage;
        currStage.GoToStage();
        currentStageDepth++;
        
        switch (currStage)
        {
            case BattleStage battle:
                battle.BattleStart();
                break;
            case BossStage boss:
                boss.BattleStart();
                return;
            case MussangStage mussang:
                mussang.BattleStart();
                break;
        }
        
        currStage.CreateNextPortal();
        while (!(currentStageDepth >= GameManager.Instance.bossStageIndex) && currStage.portalPoints.Count > Random.Range(0, 3))
            currStage.CreateNextPortal();
    }
    
    public void MoveStage(Stage stage)
    {
        if (!stageList.Move(stage))
            return;
        
        ChangeStage(stage);
        
        foreach (MultiLinkedListNode<Stage> sibling in stageList.Current.Prev.Next)
        {
            if (sibling.Value == stage) 
                continue;
            Destroy(sibling.Value.gameObject);
        }
        
        Destroy(stageList.Current.Prev.Value.gameObject);
    }
}
