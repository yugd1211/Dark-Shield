using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            playerMovement.Move(playerInputManager.InputMoveDir);
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

    // 임시로 설정 포탈 이동
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            StageManager.Instance.ChangeStage(StageManager.Instance.currStage.nextStage);
        }
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
