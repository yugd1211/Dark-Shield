using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
	// Managers
	public EnemyManager enemyManager;
	public DialogueManager dialogueManager;
	
	public List<EnemySpawnData> enemySpawnDatas;
	[SerializeField] private EnemySpawnData bossSpawnData;
	public int bossStageIndex;

	public StageManager stageManager;
	
	public GameObject playerPrefab;
	public Player player;
	public Gold gold = new Gold(0);
	
	public GameObject elementalPrefab;
	public bool isElemental;
	public bool isMussang;

	private void Init()
	{
		isElemental = false;
		isMussang = false;
		EnemyManager newEnemyManager = new GameObject("EnemyManager").AddComponent<EnemyManager>();
		newEnemyManager.transform.SetParent(transform);
		enemyManager = newEnemyManager;
		
		stageManager = FindObjectOfType<StageManager>();
		stageManager.Init();

		player = Instantiate(playerPrefab).GetComponent<Player>();
		player.Init();
		
		stageManager.player = player;
		stageManager.MoveStage(stageManager.stageList.Current.Value);
		
		DialogueManager newDialogueManager = new GameObject("DialogueManager").AddComponent<DialogueManager>();
		newDialogueManager.transform.SetParent(transform);
		dialogueManager = newDialogueManager;
		dialogueManager.Init();
	}
	
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
		Init();
	}
}
