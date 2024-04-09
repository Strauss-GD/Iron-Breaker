using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
  public int questId;             //퀘스트ID defalut : 10
  public int questActionIndex;    //퀘스트 대화 순서
  public GameObject[] questObj;   //퀘스트 아이템
  Dictionary<int, QuestData> questList;

  void Awake()
  {
    questList = new Dictionary<int, QuestData>();
    GenerateData();
  }

  void GenerateData()
  {
    questList.Add(10, new QuestData("첫 마을 방문"
                         , new int[] {1000, 2000 })); //퀘스트 순서
    questList.Add(20, new QuestData("의문의 비석 읽기"
                         , new int[] { 5000, 2000 }));
    questList.Add(30, new QuestData("퀘스트 완료"
                         , new int[] { 0 }));
  }

  //퀘스트 대화 목록
  public int GetQuestDialogIndex(int id)
  {
    return questId + questActionIndex;
  }

  public string CheckQuest()
  {
    //퀘스트 이름
    return questList[questId].questName;
  }

  public string CheckQuest(int id)
  {
    //다음 대화를 위한 카운터
    if(id == questList[questId].npcId[questActionIndex])
      questActionIndex++;

    //퀘스트 아이템-오브젝트 제어
    ControlObject();

    //지정된 퀘스트 대화가 끝났을시에 다음 퀘스트로 넘긴다.
    if (questActionIndex == questList[questId].npcId.Length)
      NextQuest();

    //퀘스트 이름
    return questList[questId].questName;
  }

  void NextQuest()
  {
    questId += 10;
    questActionIndex = 0; //초기화
  }

  public void ControlObject()
  {
    switch (questId)
    {
      //case 퀘스트 번호
      case 10 :
        if (questActionIndex == 2) questObj[0].SetActive(true);
        break;
      case 20 :
        if (questActionIndex == 0) questObj[0].SetActive(true);
        else if (questActionIndex == 1) questObj[0].SetActive(false);
        break;
    }
  }
}
