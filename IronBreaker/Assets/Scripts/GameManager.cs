using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  //외부 스크립트
  public DialogManager DM; //다이얼로그 매니저

  //다이얼로그 패널
  public GameObject dialogPanel;
  public TextMeshProUGUI dialogText;
  public Image portraitImg;

  //조사
  public GameObject scanObject;
  public bool isDialogUp; //다이어로그창이 떠있는지 아닌지
  public int dialogIndex;

  //대화창 액션
  public void DiaglogAction(GameObject scanObj)
  {
    scanObject = scanObj;
    ObjectData objData = scanObject.GetComponent<ObjectData>();
    Talk(objData.id, objData.isNpc);
    dialogPanel.SetActive(isDialogUp);
  }

  void Talk(int id, bool isNpc)
  {
    string dialogData = DM.GetDiaglog(id, dialogIndex);
    if(dialogData == null) //대화내용이없을시 대화창 닫힘
    {
      isDialogUp = false;
      dialogIndex = 0;     //초기화
      return;
    }

    if (isNpc) //NPC
    {
      dialogText.text = dialogData.Split(':')[0];

      portraitImg.sprite = DM.GetPortrait(id, int.Parse(dialogData.Split(':')[1])); //int.Parse는 문자열내에 있는 문자가 사실 문자가아닌 int형 숫자라고 강제 형변환시켜주는것.
      portraitImg.color = new Color(1, 1, 1, 1);
    }
    else       //오브젝트
    {
      dialogText.text = dialogData;

      portraitImg.color = new Color(1, 1, 1, 0);
    }
    isDialogUp = true;
    dialogIndex++;
  }
}
