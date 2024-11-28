using UnityEngine;

public class Player : MonoBehaviour
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

    private void Start()
    {
        playerStateMachine.Init(playerStateMachine.idleState);
    }

    private void Update()
    {
        if (playerStateMachine.CurState == playerStateMachine.walkState/*!= playerStateMachine.skill1State && playerStateMachine.CurState != playerStateMachine.dashState*/)
        {
            playerMovement.Move(new Vector3(playerInputManager.InputMoveDir.x, 0, playerInputManager.InputMoveDir.y));
        }
        playerStateMachine.OnUpdate();
    }

    private void Init()
    {
        playerStateMachine = new StateMachine(this);
        curWeopon = GetComponent<Weapon>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAnimator = GetComponent<Animator>();
    }

    #region 애니메이션 이벤트 함수 ENDXX
    public void EndDash()
    {
        playerStateMachine.dashState.EndDash();
    }

    public void EndSkill1()
    {
        playerStateMachine.skill1State.EndSkill1();
    }

    public void EndSkill2()
    {
        playerStateMachine.skill2State.EndSkill2();
    }

    public void EndHit()
    {
        playerStateMachine.hitState.EndHit();
    }
    #endregion
}
