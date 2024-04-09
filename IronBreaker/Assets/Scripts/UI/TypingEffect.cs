using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
  public float CharPerSeconds;        //초당 글자
  public GameObject EndCursor;      //대화 패널 우측 하단에 나오는 커서

  string targetMsg;
  [SerializeField] TextMeshProUGUI msgText;
  [SerializeField] AudioSource audioSource;

  int index;
  public bool isTyping;    //파이팅이 진행중인가

  public void SetMsg(string msg)
  {
    if (isTyping)
    {
      msgText.text = targetMsg;
      CancelInvoke();
      EffectEnd();
    }
    else
    {
      targetMsg = msg;
      EffectStart();
    }
  }

  //타이핑 효과 시작
  void EffectStart()
  {
    msgText.text = "";
    index = 0;
    EndCursor.SetActive(false);

    isTyping = true;

    Invoke("Effecting", 1 / CharPerSeconds);
  }

  //타이핑 효과 진행중
  void Effecting()
  {
    //End Effect
    if(msgText.text == targetMsg)
    {
      EffectEnd();
      return;
    }

    msgText.text += targetMsg[index];
    //사운드
    if (targetMsg[index] != ' ' || targetMsg[index] != ',') audioSource.Play();
    index++;

    Invoke("Effecting", 1 / CharPerSeconds);
  }

  //타이핑 효과 끝
  void EffectEnd()
  {
    isTyping = false;
    EndCursor.SetActive(true);
  }
}

/* 코루틴으로 작동하게 변경 예정.
  if (Input.GetKeyDown(KeyCode.LeftShift)) GetKey형식이 아닌, 버튼을 추가할 예정. boolean으로 조정
          TypingManager.Instance.TypingSpeed = true;

      if (Input.GetKeyUp(KeyCode.LeftShift))
          TypingManager.Instance.TypingSpeed = false;


  private IEnumerator TypingSpeedMethod()
  {
      while (coroutineStoper)
      {
          if (TypingSpeed)
              CPSRepetition = CPSRepetitionMax;
          else
              CPSRepetition = CPSRepetitionMin;

          Effecting();

          yield return new WaitForSeconds(1f / CPSRepetition);

      }
  }
 */