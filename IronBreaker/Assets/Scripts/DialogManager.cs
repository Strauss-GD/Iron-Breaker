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
    //Dialog Data
    //NPC(w) : 1000  NPC(m) : 2000
    //Box : 100, desk : 200
    dialogData.Add(1000, new string[] { "안녕?:2", "여기는 처음이지?:1"});
    dialogData.Add(2000, new string[] { "꺼져:3"});
    dialogData.Add(100, new string[] { "평범 나무상자"});
    dialogData.Add(200, new string[] { "게시판이다" });

    //Quest Dialog Data
    dialogData.Add(10 + 1000, new string[] {  "어서와. :0",
                                              "이 마을에 놀라운 전설이 있다는데:1",
                                              "그 전설에 대한 조사를 해줬으면해.:2",
                                              "동쪽에 있는 마을 입구에 조사관이 있을꺼야 그 분이 자세히 알려줄꺼야.:2"
    });
    dialogData.Add(11 + 2000, new string[] {  "나는 이 마을에 전설에대해 조사하고있는 사람이네.:1",
                                              "전설에 대한것 말인가? 저 입구를 막고있는 비석에 모든것이 적혀있다하더군.:1",
                                              "하지만 나는 읽을수 없어 곤란한 참이야...:3",
                                              "내 능력외의 건인듯하네, 자네도 읽을 수 있다면 한번 읽어보게나:1"
    });

    dialogData.Add(20 + 5000, new string[] {  "나는 이 세계의 사람이 아니다.",
                                              "평범하게 살고있던 나는 갑자기 이 세상에서 눈을 떴다.",
                                              "내가 살던 곳에 비해 아무것도 아무일도 일어나지않는 이 지루한 세상에서",
                                              "나 골드 로저는 눈을 감는다."
    });
    dialogData.Add(21 + 2000, new string[] {  "뭐? 그런 내용이 써있는 비석이었다고?:3",
                                              "이건 전설도 뭣도 아니잖아 젠장!:3",
                                              "상부에는 뭐라고 보고하지...:3",
                                              "이런 내가 정신이 없었군. 고맙네 젊으니:2"
    });

    //Portrait Data
    //0:Idle, 1:Talk, 2:Smile, 3:Angry
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
    if (!dialogData.ContainsKey(id))
    {
      if (!dialogData.ContainsKey(id - id % 10)){
        //퀘스트 맨 처음 대사마저 없을 때, 기본 대사를 가져온다.
        if (dialogIndex == dialogData[id - id % 100].Length) return null;
        return dialogData[id - id % 100][dialogIndex];
      }
      else
      {
        //해당 퀘스트 진행 순서 대사가 없을 때 퀘스트 맨 처음 대사를 가지고 온다.
        if (dialogIndex == dialogData[id - id % 10].Length) return null;
        return dialogData[id - id % 10][dialogIndex]; //[id - (id % 현재 퀘스트 대화 카운터)]
      }
    }

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
 