using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
  public static CanvasManager instance;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject); // 이 오브젝트를 씬 전환 시 파괴되지 않도록 설정
    }
    else if (instance != this)
    {
      Destroy(gameObject); // 중복 생성 방지
    }
  }

}
