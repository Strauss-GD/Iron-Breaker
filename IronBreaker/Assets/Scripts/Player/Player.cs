using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//CharaterController2D 참고
public enum GroundType
{
  NONE,
  SOFT,
  HARD,
}

public class Player : MonoBehaviour
{
  [Header("캐릭터 스테이터스")]
  public float speed; //테스트용으로했을때 1초에 4개타일을 움직이려면 3.5~4이내의 값이 필요했음.

  [SerializeField] private Rigidbody2D playerRigid;

  private Vector2 movementInput;

  void Update()
  {

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
      playerRigid.velocity = movementInput * speed;
    }
  }

  //Input System에 의한 대쉬
  public void onDash(InputAction.CallbackContext context)
  {
    if (context.performed) //Action type이 "button"일 경우 키가 눌렸는지 체크.
    {
      //점프 로직
    }
  }

  //애니메이션 추후 추가 예정
  void PlayerAnimation()
  {
    //https://www.youtube.com/watch?v=bZVa6C6vRBQ&list=PLO-mt5Iu5TeYfyXsi6kzHK8kfjPvadC5u&index=2&ab_channel=%EA%B3%A8%EB%93%9C%EB%A9%94%ED%83%88
    //참고
  }


}
