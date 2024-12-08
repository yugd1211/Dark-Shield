using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
	// Managers
	public EnemyManager enemyManager;
	public DialogueManager dialogueManager;
	public UpgradeManager upgradeManager;
	
	public List<EnemySpawnData> enemySpawnDatas;
	[SerializeField] private EnemySpawnData bossSpawnData;
	public int bossStageIndex;
	// StageManager는 임시로 생성이 아닌 참조로 할당
	// Stage랜덤할당이 되면 StageManager도 GameManager가 생성하고, Stage도 구성하게끔 할 예정
	public StageManager stageManager;
	
	public GameObject playerPrefab;
	public Player player;
	public Gold gold = new Gold(0);

	private void Init()
	{
		EnemyManager newEnemyManager = new GameObject("EnemyManager").AddComponent<EnemyManager>();
		newEnemyManager.transform.SetParent(transform);
		enemyManager = newEnemyManager;
		
		stageManager = FindObjectOfType<StageManager>();
		stageManager.Init();

		player = Instantiate(playerPrefab).GetComponent<Player>();
		player.Init();
		
		stageManager.player = player;
		stageManager.ChangeStage(stageManager.currStage);
		
		DialogueManager newDialogueManager = new GameObject("DialogueManager").AddComponent<DialogueManager>();
		newDialogueManager.transform.SetParent(transform);
		dialogueManager = newDialogueManager;
		dialogueManager.Init();
	}
}


// Singleton
public partial class GameManager
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance) 
				return _instance;
			_instance = FindObjectOfType<GameManager>();
			if (_instance)
				return _instance;
			GameObject container = new GameObject("GameManager");
			_instance = container.AddComponent<GameManager>();
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(gameObject);
		Init();
	}
}
