using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Player player;
    
    public Stage[] stages;
    public Stage currStage;

    private void LinkStage()
    {
        for (int i = 0; i < stages.Length - 1; i++)
        {
            stages[i].nextStage = stages[i + 1];
        }
    }

    private void Init()
    {
        player = FindObjectOfType<Player>();
        currStage = stages[0];
        currStage.nextStage = currStage;
        LinkStage();
        ChangeStage(currStage);
    }
    
    private void Start()
    {
        Init();
    }

    public void ChangeStage(Stage stage)
    {
        currStage = stage;
        currStage.Init(this);
    }
}
