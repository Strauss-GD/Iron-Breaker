using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
  Dictionary<int, string[]> dialogData;  //고유ID, 대화열
  Dictionary<int, Sprite> portraiteData; //NPC 감정별 이미지 초상화

  public Sprite[] portraitArr;

  void Awake()
  {
    dialogData = new Dictionary<int, string[]>();
    portraiteData = new Dictionary<int, Sprite>();
    GenerateData();
  }

  void GenerateData()
  {
    dialogData.Add(1000, new string[] { "안녕?:2", "여기는 처음이지?:1"});
    dialogData.Add(2000, new string[] { "꺼져:3"});
    dialogData.Add(100, new string[] { "평범 나무상자"});
    dialogData.Add(200, new string[] { "게시판이다" });

    portraiteData.Add(1000 + 0, portraitArr[0]);
    portraiteData.Add(1000 + 1, portraitArr[1]);
    portraiteData.Add(1000 + 2, portraitArr[2]);
    portraiteData.Add(1000 + 3, portraitArr[3]);
    portraiteData.Add(2000 + 0, portraitArr[4]);
    portraiteData.Add(2000 + 1, portraitArr[5]);
    portraiteData.Add(2000 + 2, portraitArr[6]);
    portraiteData.Add(2000 + 3, portraitArr[7]);
  }

  public string GetDiaglog(int id, int dialogIndex)
  {
    if (dialogIndex == dialogData[id].Length) return null;
    else
    return dialogData[id][dialogIndex];
  }


  //초상화 가져오기
  public Sprite GetPortrait(int id, int portraitIndex)
  {

    return portraiteData[id + portraitIndex];
  }
}
