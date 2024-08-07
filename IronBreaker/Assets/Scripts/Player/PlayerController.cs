using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] public GameManager GM;
  protected Player player;
  [SerializeField] Harpoon harpoon;

  // Animation
  private SpriteRenderer mySpriteRender;
  Animator anim;
  float h, v;

  // Move
  public Vector2 movementInput { get; private set; }

  // Mouse Info
  Vector3 target;

  // Physics
  [SerializeField] private Rigidbody2D playerRigid;
  GameObject scanObj;

  // Dash
  private bool isDashing = false;
  private float dashSpeed = 10f;
  private float dashTime = 0.2f;
  private Vector2 dashDirection;

  // Input Actions
  private PlayerControls playerInputActions;
  private InputAction moveAction;
  private InputAction shootAction;
  private InputAction dashAction;

  void Awake()
  {
    player = GetComponent<Player>();
    playerRigid = GetComponent<Rigidbody2D>();
    mySpriteRender = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();

    // Input Actions 초기화
    playerInputActions = new PlayerControls();
    moveAction = playerInputActions.Player.Move;
    shootAction = playerInputActions.Player.Shot;
    dashAction = playerInputActions.Player.Dash;

    // 이동 및 사격 이벤트 연결
    moveAction.performed += OnMove;
    moveAction.canceled += OnMove;
    shootAction.performed += OnShot;
    dashAction.performed += OnDash;
  }

  void OnEnable()
  {
    moveAction.Enable();
    shootAction.Enable();
    dashAction.Enable();
  }

  void OnDisable()
  {
    moveAction.Disable();
    shootAction.Disable();
    dashAction.Disable();
  }

  void Update()
  {
    PlayerAnimation();
    OnScan();
  }

  void FixedUpdate()
  {
    if (!isDashing)
    {
      MovePlayer();
    }
    Debug.DrawRay(playerRigid.position, GetMouseWorldPosition() - transform.position, new Color(0, 1, 0));
    OnSearch();
  }

  // Input System에 의한 이동
  public void OnMove(InputAction.CallbackContext context)
  {
    Vector2 input = context.ReadValue<Vector2>();
    if (!GM.isDialogUp && !isDashing)
    {
      movementInput = input;
    }
    else
    {
      movementInput = Vector2.zero;
    }
  }

  // 플레이어 이동
  private void MovePlayer()
  {
    playerRigid.velocity = movementInput * player.MoveSpeed;
  }

  // Input System에 의한 사격과 차징 사격
  public void OnShot(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      if (context.interaction is HoldInteraction)
      {
        harpoon.CreateChargedProjectile();
      }
      else if (context.interaction is PressInteraction)
      {
        harpoon.CreateProjectile();
      }
    }
  }

  // 마우스 좌표 따기
  protected Vector3 GetMouseWorldPosition()
  {
    Vector2 mousePos = Mouse.current.position.ReadValue();
    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
    worldMousePos.z = 0;
    return worldMousePos;
  }

  // 조사 탐색
  void OnSearch()
  {
    RaycastHit2D rayHit = Physics2D.Raycast(playerRigid.position, GetMouseWorldPosition() - transform.position, 1.0f, LayerMask.GetMask("Object"));

    if (rayHit.collider != null)
    {
      scanObj = rayHit.collider.gameObject;
    }
    else scanObj = null;
  }

  void OnScan()
  {
    if (Input.GetButtonDown("Jump") && scanObj != null) GM.DiaglogAction(scanObj);
  }

  // 구르기 기능 구현
  public void OnDash(InputAction.CallbackContext context)
  {
    if (context.performed && !isDashing)
    {
      StartCoroutine(Dash());
    }
  }

  private IEnumerator Dash()
  {
    isDashing = true;
    dashDirection = movementInput.normalized;

    // 무적 상태 설정
    player.SetInvincible(true);

    float dashEndTime = Time.time + dashTime;

    while (Time.time < dashEndTime)
    {
      playerRigid.velocity = dashDirection * dashSpeed;
      yield return null;
    }

    // 무적 상태 해제
    player.SetInvincible(false);

    isDashing = false;
  }

  // Animation
  void PlayerAnimation()
  {
    h = GM.isDialogUp ? 0 : Input.GetAxisRaw("Horizontal");
    v = GM.isDialogUp ? 0 : Input.GetAxisRaw("Vertical");

    if (anim.GetInteger("hAxisRaw") != h)
    {
      anim.SetBool("isChange", true);
      anim.SetInteger("hAxisRaw", (int)h);
    }
    else if (anim.GetInteger("vAxisRaw") != v)
    {
      anim.SetBool("isChange", true);
      anim.SetInteger("vAxisRaw", (int)v);
    }
    else anim.SetBool("isChange", false);
  }
}
