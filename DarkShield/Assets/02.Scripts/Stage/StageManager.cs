using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Player player;
    
    public List<Stage> stages = new List<Stage>();
    
    public GameObject[] battleStagePrefabs;
    public GameObject shopStagePrefab;
    public GameObject bossStagePrefab;
    public Stage currStage;

    private void LinkStage()
    {
        for (int i = 0; i < stages.Count - 1; i++)
        {
            stages[i].nextStage = stages[i + 1];
        }
    }

    private void Init()
    {
        player = FindObjectOfType<Player>();
        CreateStage();
        currStage = stages[0];
        LinkStage();
        ChangeStage(currStage);
    }
    
    private void Start()
    {
        Init();
    }

    private void CreateStage()
    {
        for (int i = 0; i < 10; i++)
        {
            int ran = Random.Range(0, 100);
            GameObject newStage;
            if (ran <= 10)
                newStage = Instantiate(shopStagePrefab);
            else 
                newStage = Instantiate(battleStagePrefabs[Random.Range(0, battleStagePrefabs.Length)]);
            stages.Add(newStage.GetComponent<Stage>());
            stages[i].transform.position = new Vector3(0, 0, i * 100);
            stages[i].transform.SetParent(transform);
        }
    }

    public void ChangeStage(Stage stage)
    {
        currStage = stage;
        currStage.Init(this);
    }
}
