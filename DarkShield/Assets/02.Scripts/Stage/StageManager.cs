using System.Collections.Generic;
using UnityEngine;
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
    public MultiLinkedList<Stage> stageList = new MultiLinkedList<Stage>();
    public GameObject[] battleStagePrefabs;
    public GameObject shopStagePrefab;
    public GameObject bossStagePrefab;
    public GameObject startStagePrefab;
    public GameObject mussangStagePrefab;
    public int stageCount = 0;
    public int currentStageDepth = 0;
    
    public void Init()
    {
        player = FindObjectOfType<Player>();
        stageList.Add(CreateStage());
    }
    
    public Stage CreateStage()
    {
        int ran = Random.Range(0, 100);
        GameObject newStage;
        if (stageCount == 0)
            newStage = Instantiate(startStagePrefab);
        else if (currentStageDepth >= GameManager.Instance.bossStageIndex)
            newStage = Instantiate(bossStagePrefab);
        else if (GameManager.Instance.isElemental && !GameManager.Instance.isMussang)
        {
            GameManager.Instance.isMussang = true;
            newStage = Instantiate(mussangStagePrefab);
        }
        else if (ran <= 20)
            newStage = Instantiate(shopStagePrefab);
        else 
            newStage = Instantiate(battleStagePrefabs[Random.Range(0, battleStagePrefabs.Length)]);
        
        newStage.transform.position = new Vector3(0, 0, stageCount * 100);
        newStage.transform.SetParent(transform);
        
        Stage stage = newStage.GetComponent<Stage>();
        stageList.Add(stage);
        stageCount++;
        stage.Init(this);
        return stage;
    }
    
    public void MoveStage(Stage stage)
    {
        if (!stageList.Move(stage))
            return;
        
        stageList.Current.Value = stage;
        stageList.Current.Value.GoToStage();
        currentStageDepth++;
        
        switch (stageList.Current.Value)
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
        
        stageList.Current.Value.CreateNextPortal();
        while (!(currentStageDepth >= GameManager.Instance.bossStageIndex) && stageList.Current.Value.portalPoints.Count > Random.Range(0, 3))
            stageList.Current.Value.CreateNextPortal();
        
        foreach (MultiLinkedListNode<Stage> prev in stageList.Current.Prev.Next)
        {
            if (prev.Value == stage) 
                continue;
            Destroy(prev.Value.gameObject);
        }
        
        if (stageList.Current.Prev.Value != stageList.Current.Value)
            Destroy(stageList.Current.Prev.Value.gameObject);
    }
}
