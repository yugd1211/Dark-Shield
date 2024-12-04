using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
	// Managers
	[HideInInspector] public EnemyManager enemyManager;
	[HideInInspector] public DialogueManager dialogueManager;
	[HideInInspector] public CoinManager coinManager;
	
	[SerializeField] private List<EnemySpawnData> enemySpawnDatas;
	
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
		enemyManager.Init(enemySpawnDatas[0]);
		
		DialogueManager newDialogueManager = new GameObject("DialogueManager").AddComponent<DialogueManager>();
		newDialogueManager.transform.SetParent(transform);
		dialogueManager = newDialogueManager;
		
		stageManager = FindObjectOfType<StageManager>();
		
		// Player도 GameManager에서 생성하게끔 할 예정
		// Player Init에 얽혀있는 것들이 너무 많아서 상의후 결정해햐할듯
		// 예를들면 Weapon의 Init을 Awake에서 하는 것
		player = FindObjectOfType<Player>();
		// player = Instantiate(playerPrefab).GetComponent<Player>();
		player.Init();
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
