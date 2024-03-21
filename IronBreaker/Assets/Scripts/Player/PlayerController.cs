using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

//이 스크립트에는 "입력 및 컨트롤"에 관한 내용들이 들어갑니다.
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
  protected Player player;
  [SerializeField] Harpoon harpoon;

  //Animation
  private SpriteRenderer mySpriteRender;
  Animator anim;
  float h, v;

  //Move
  public Vector2 movementInput { get; private set; }

  //Direction
  Vector3 dirVec;

  //Mouse Info
  Vector3 target;

  //Physics
  [SerializeField] private Rigidbody2D playerRigid;
  GameObject scanObj;

  void Awake()
  {
    player = GetComponent<Player>();
    playerRigid = GetComponent<Rigidbody2D>();
    mySpriteRender = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }

  void Start()
  {
    
  }

  void Update()
  {
    //RotationPlayer();
    PlayerAnimation();
    GetDirection();
    OnScan();
  }

  void FixedUpdate()
  {
    //AdjustPlayerFacingDirection();
    Debug.DrawRay(playerRigid.position, dirVec * 0.7f, new Color(0, 1, 0));
    OnSearch();
  }

  //Input System에 의한 이동
  public void onMove(InputAction.CallbackContext context)
  {
    Vector2 input = context.ReadValue<Vector2>();
    if (input != null)
    {
      movementInput = new Vector2(input.x, input.y);
      playerRigid.velocity = movementInput * player.MoveSpeed;
    }
  }

  //Input System에 의한 사격과 차징 사격
  public void onShot(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      //차지 공격
      if (context.interaction is HoldInteraction)
      {
        harpoon.CreateChargedProjectile();
      }
      //기본 공격
      else if (context.interaction is PressInteraction)
      {
        harpoon.CreateProjectile();
      }
    }
  }

  // 마우스 좌표 따기
  protected Vector3 GetMouseWorldPosition()
  {
    Vector2 mousePos = Input.mousePosition;
    Vector3 target = Camera.main.WorldToScreenPoint(mousePos);
    return target;
  }

  //정면 확인
  protected void GetDirection()
  {
    bool hDown = Input.GetButtonDown("Horizontal");
    bool vDown = Input.GetButtonDown("Vertical");
    //bool hUp = Input.GetButtonUp("Horizontal");
    //bool vUp = Input.GetButtonUp("Vertical");

    if (vDown && v == 1)       dirVec = Vector3.up;
    else if (vDown && v == -1) dirVec = Vector3.down;
    else if (hDown && h == -1) dirVec = Vector3.left;
    else if (hDown && h == 1)  dirVec = Vector3.right;
  }

  //조사 탐색
  void OnSearch()
  {
    RaycastHit2D rayHit = Physics2D.Raycast(playerRigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

    if(rayHit.collider != null)
    {
      scanObj = rayHit.collider.gameObject;
    }
    else scanObj = null;
  }

  void OnScan()
  {
    if (Input.GetButtonDown("Jump") && scanObj != null) Debug.Log("This is : " + scanObj.name);
  }

  //마우스 방향으로 캐릭터 회전
  private void RotationPlayer()
  {
    Vector2 newPos = GetMouseWorldPosition() - transform.position;
    float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, rotZ);
  }

  //Input System에 의한 대쉬
  public void onDash(InputAction.CallbackContext context)
  {
    if (context.performed) //Action type이 "button"일 경우 키가 눌렸는지 체크.
    {
      //점프 로직
    }
  }

  //마우스 방향에 따른 플레이어 대면 방향 조정(현재 사용안함)
  private void AdjustPlayerFacingDirection()
  {
    Vector3 mousePos = Input.mousePosition;
    Vector3 target = Camera.main.WorldToScreenPoint(transform.position);
    if(mousePos.x < target.x)
    {
      mySpriteRender.flipX = true;
    }
    else
    {
      mySpriteRender.flipX = false;
    }
  }

  //Animation
  void PlayerAnimation()
  {
    h = Input.GetAxisRaw("Horizontal");
    v = Input.GetAxisRaw("Vertical");


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
