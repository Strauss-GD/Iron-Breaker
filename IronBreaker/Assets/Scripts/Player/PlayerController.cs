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
  Animator anim;
  float h, v;

  //Move
  public Vector2 movementInput { get; private set; }

  //Mouse Info
  Vector3 target;

  //Physics
  [SerializeField] private Rigidbody2D playerRigid;
  

  void Awake()
  {
    player = GetComponent<Player>();
    playerRigid = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  void Start()
  {
    
  }

  void Update()
  {
    RotationPlayer();
    PlayerAnimation();
  }

  void FixedUpdate()
  {
    
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
        harpoon.CreateProjectile();
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
    Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);
    return target;
  }

  //마우스 방향으로 캐릭터 회전
  void RotationPlayer()
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

  //Animation
  void PlayerAnimation()
  {
    h = Input.GetAxisRaw("Horizontal");
    v = Input.GetAxisRaw("Vertical");
/*
    bool hDown = Input.GetButtonDown("Horizontal");
    bool vDown = Input.GetButtonDown("Vertical");
    bool hUp = Input.GetButtonUp("Horizontal");
    bool vUp = Input.GetButtonUp("Vertical");

    if (hDown || vUp) isHorizonMove = true;
    else if (vDown || hUp) isHorizonMove = false;
    else if (hUp || vUp) isHorizonMove = h != 0;
    */

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
