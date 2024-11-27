using UnityEngine;

public partial class StageManager : MonoBehaviour
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

public partial class StageManager
{
    // 임시 싱글톤
    private static StageManager _instance;
    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageManager>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("StageManager");
                    _instance = container.AddComponent<StageManager>();
                }
            }
            return _instance;
        }
    }
}
