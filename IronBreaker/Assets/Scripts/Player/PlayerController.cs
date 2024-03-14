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
  
  //Animation
  Animator anim;
  float h, v;
  bool isHorizonMove;

  public Vector2 movementInput { get; private set; }

  [SerializeField] private Rigidbody2D playerRigid;
  

  void Awake()
  {
    player = GetComponent<Player>();
    playerRigid = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  void Update()
  {
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


  //Input System에 의한 공격과 강공격
  public void onShot(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      //차지 공격
      if (context.interaction is HoldInteraction)
      {

      }
      //기본 공격
      else if (context.interaction is PressInteraction)
      {

      }
    }
  }

  // 마우스 좌표 따기
  protected Vector2 GetMouseWorldPosition()
  {
    Vector2 mousePosition = Mouse.current.position.ReadValue();
    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
    Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 5f);  // Raycast 시각화

    if (Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity))
    {
      Vector2 target = HitInfo.point;
      Vector2 myPosition = new Vector3(transform.position.x, transform.position.y);
      target.Set(target.x, target.y);
      return (target - myPosition).normalized;
    }

    return Vector2.zero;
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
