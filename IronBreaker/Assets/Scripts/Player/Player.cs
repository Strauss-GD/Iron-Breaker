using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  //플레이어의 스피드
  public float Speed;

  [SerializeField] Rigidbody2D rigid;
  float h, v;
  bool isHorizonMove;

  void Update()
  {
    //이동 값 설정
    h = Input.GetAxisRaw("Horizontal");
    v = Input.GetAxisRaw("Vertical");

    //Check Button Down & Up
    bool hDown = Input.GetButtonDown("Horizontal");
    bool vDown = Input.GetButtonDown("Vertical");
    bool hUp = Input.GetButtonUp("Horizontal");
    bool vUp = Input.GetButtonUp("Vertical");

    //Check Horizontal Move
    if (hDown || vUp) isHorizonMove = true;
    else if (vDown || hUp) isHorizonMove = false;
  }

  void FixedUpdate()
  {
    //이동
    Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
    rigid.velocity = moveVec * Speed;
  }
}
