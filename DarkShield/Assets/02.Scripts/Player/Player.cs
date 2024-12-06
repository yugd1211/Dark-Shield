using UnityEngine;

public class Player : Unit
{
    public StateMachine playerStateMachine;
    public Weapon curWeopon;
    public PlayerMovement playerMovement;
    public PlayerInputManager playerInputManager;
    public PlayerHealth playerHealth;
    public Animator playerAnimator;
    public MouseLook mouseLookDir;
    public Dash playerDash;
    public PlayerStat playerStat;
    public AnimationEventEffects playerAnimationEventEffects;

    [SerializeField] private Transform rightHand;
    [SerializeField] private GameObject swordPrefab;

    //무기 장착
    public bool isEquip;

    private void Update()
    {
        if (playerStateMachine.CurState == playerStateMachine.walkState)
        {
            Move(new Vector3(playerInputManager.InputMoveDir.x, 0, playerInputManager.InputMoveDir.y).normalized);
            playerMovement.Rotate(playerInputManager.InputMoveDir);
        }
        playerStateMachine.OnUpdate();
    }

    public void Init()
    {
        playerStateMachine = new StateMachine(this);
        playerMovement = GetComponent<PlayerMovement>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAnimator = GetComponent<Animator>();
        mouseLookDir = GetComponent<MouseLook>();
        playerDash = GetComponent<Dash>();
        playerStat = GetComponent<PlayerStat>();
        playerAnimationEventEffects = GetComponent<AnimationEventEffects>();
        playerStateMachine.Init(playerStateMachine.idleState);
        MoveSpeed = 5;
    }

    public void ChangeWeapon(WeaponChange weapon)
    {
        playerAnimator.runtimeAnimatorController = weapon.animController;
        curWeopon = Instantiate(weapon.weaponPrefab, rightHand).GetComponent<Weapon>();
        curWeopon.Init(this);
        isEquip = true;
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

    public bool isDamaged = true;
    public override void TakeDamage(float amount, bool isHit)
    {
        if (!isDamaged)
            return;
        playerHealth.TakeDamage(amount, isHit);
    }

    public override void Move(Vector3 dir)
    {
        playerMovement.Move(dir * MoveSpeed);
    }
    
    public void DashUpgrade()
    {
        playerDash.maxDashCount++;
    }
}
