using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  public GameObject dialogPanel;
  public TextMeshProUGUI dialogText;
  public GameObject scanObject;
  public bool isDialogUp; //다이어로그창이 떠있는지 아닌지

  public void Action(GameObject scanObj)
  {
    if (isDialogUp) //대화창 닫기 액션
    {
      isDialogUp = false;
    }
    else
    {
      isDialogUp = true;
      scanObject = scanObj;
      dialogText.text = "평범한" + scanObject.name + "이다.";
    }
    dialogPanel.SetActive(isDialogUp);
  }
}
