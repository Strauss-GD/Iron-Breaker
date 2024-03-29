using System.Collections;
using System.Collections.Generic;

//퀘스트 데이터 구조체
public class QuestData
{
  public string questName;
  public int[] npcId;

  public QuestData(string name, int[] npc)
  {
    questName = name;
    npcId = npc;
  }
}
