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

    private void Update()
    {
        if (playerStateMachine.CurState == playerStateMachine.walkState)
        {
            playerMovement.Move(new Vector3(playerInputManager.InputMoveDir.x, 0, playerInputManager.InputMoveDir.y));
            playerMovement.Rotate(playerInputManager.InputMoveDir);
        }
        playerStateMachine.OnUpdate();
    }

    private void Init()
    {
        playerStateMachine = new StateMachine(this);
        //curWeopon = GetComponent<Weapon>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAnimator = GetComponent<Animator>();
        playerStateMachine.Init(playerStateMachine.idleState);
    }

    public float moveDistance;

    public void CustomTeleport()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var rend in renderers)
        {
            rend.enabled = false;
        }
        transform.position += transform.forward * moveDistance;

        foreach (var rend in renderers)
        {
            rend.enabled = true;
        }
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
}
