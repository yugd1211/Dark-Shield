using UnityEngine;

public class Player : Unit
{
	public StateMachine playerStateMachine;
	public Weapon curWeopon;
	public PlayerMovement playerMovement;
	public PlayerInputManager playerInputManager;
	public PlayerHealth playerHealth;
	public Animator playerAnimator;

	[SerializeField] private Transform rightHand;
	[SerializeField] private GameObject swordPrefab;

	private void Awake()
	{
		Init();
		Instantiate(swordPrefab, rightHand);
	}

	private void Update()
	{
		if (playerStateMachine.CurState == playerStateMachine.walkState)
		{
			Move(new Vector3(playerInputManager.InputMoveDir.x, 0, playerInputManager.InputMoveDir.y).normalized);
			playerMovement.Rotate(playerInputManager.InputMoveDir);
		}
		playerStateMachine.OnUpdate();
	}

	private void Init()
	{
		playerStateMachine = new StateMachine(this);
		playerMovement = GetComponent<PlayerMovement>();
		playerInputManager = GetComponent<PlayerInputManager>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAnimator = GetComponent<Animator>();
		playerStateMachine.Init(playerStateMachine.idleState);
		MoveSpeed = 5;
	}

	public float moveDistance;

	public void CustomTeleport()
	{
		transform.position += transform.forward * moveDistance;
	}

	#region 애니메이션 이벤트 함수 ENDXX
	public void EndDash()
	{
		playerStateMachine.dashState.EndDash();
	}

	public void EndSkill()
	{
		playerStateMachine.skillState.EndSkill();
	}

	public void EndHit()
	{
		playerStateMachine.hitState.EndHit();
	}

	#endregion

	public override void TakeDamage(float amount, bool isHit)
	{
		playerHealth.TakeDamage(amount, isHit);
	}

	public override void Move(Vector3 dir)
	{
		playerMovement.Move(dir * MoveSpeed);
	}
}
