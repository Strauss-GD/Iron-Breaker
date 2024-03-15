﻿using UnityEngine;

//카메라 매니저
public class CamaraManager : MonoBehaviour
{

  public GameObject target; // 카메라가 따라갈 대상
  public float moveSpeed; // 카메라가 따라갈 속도
  private Vector3 targetPosition; // 대상의 현재 위치

  void Update()
  {
    if (target.gameObject != null)
    {
      
      // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
      targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

      // vectorA -> B까지 T의 속도로 이동
      this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
  }
}