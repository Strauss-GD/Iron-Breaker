using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  //외부 스크립트
  public DialogManager DM; //다이얼로그 매니저
  public QuestManager QM;  //퀘스트 매니저
  public TypingEffect typingEffect;  //타이핑 이펙트 스크립트

  //다이얼로그 패널
  public Animator dialogPanel;
  public Animator portraitAnim;
  public TextMeshProUGUI dialogText;
  public TextMeshProUGUI nameText;
  public Image portraitImg;
  public Sprite beforePortrait;   //초상화 변경 전의 스프라이트

  //UI 패널
  public GameObject menuSet;
  public TextMeshProUGUI questText;

  //조사
  public GameObject scanObject;
  public bool isDialogUp; //다이어로그창이 떠있는지 아닌지
  public int dialogIndex;

  //플레이어
  public Player player;

  void Start()
  {
    GameLoad();
    questText.text = QM.CheckQuest();
  }

  void Update()
  {
    if (Input.GetButtonDown("Cancel"))
    {
      if (menuSet.activeSelf) menuSet.SetActive(false);
      else menuSet.SetActive(true);
    }
  }

  //대화창 액션
  public void DiaglogAction(GameObject scanObj)
  {
    //정면에 있는 오브젝트 정보 취득
    scanObject = scanObj;
    ObjectData objData = scanObject.GetComponent<ObjectData>();
    Dialog(objData.id, objData.isNpc);
    
    //다이얼로그 패널 활성 비활성화
    dialogPanel.SetBool("isShow", isDialogUp);


  }

  void Dialog(int id, bool isNpc)
  {
    //Set Dialog Data
    int questDialogIndex = 0;
    string dialogData = "";

    if (typingEffect.isTyping)
    {
      typingEffect.SetMsg("");
      return;
    }
    else
    {
      questDialogIndex = QM.GetQuestDialogIndex(id);
      dialogData = DM.GetDiaglog(id + questDialogIndex, dialogIndex);
    }

    if (dialogData == null) //대화내용이없을시 대화창 닫힘
    {
      isDialogUp = false;
      dialogIndex = 0;     //초기화
      nameText.text = " ";
      questText.text = QM.CheckQuest(id);
      return;
    }

    if (isNpc) //NPC
    {
      typingEffect.SetMsg(dialogData.Split(':')[0]);

      portraitImg.sprite = DM.GetPortrait(id, int.Parse(dialogData.Split(':')[1])); //int.Parse는 문자열내에 있는 문자가 사실 문자가아닌 int형 숫자라고 강제 형변환시켜주는것.
      portraitImg.color = new Color(1, 1, 1, 1);
      nameText.text = scanObject.name;
      
      //초상화 스프라이트가 다를때
      if (beforePortrait != portraitImg.sprite)
      {
        portraitAnim.SetTrigger("doChange");
        beforePortrait = portraitImg.sprite;
      }
    }
    else       //오브젝트
    {
      typingEffect.SetMsg(dialogData);

      //초상화 투명
      portraitImg.color = new Color(1, 1, 1, 0);
    }
    isDialogUp = true;
    dialogIndex++;
  }

  //게임 저장하기
  public void GameSave()
  {
    //플레이어 스탯 추가시 추가해줄것.
    PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
    PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
    PlayerPrefs.SetFloat("QuestId", QM.questId);
    PlayerPrefs.SetFloat("QuestActionIndex", QM.questActionIndex);
    PlayerPrefs.Save();

    menuSet.SetActive(false);
  }

  //게임 불러오기
  public void GameLoad()
  {
    //Save 파일이 존재하지 않을 경우
    if (!PlayerPrefs.HasKey("PlayerX")) return;

    float x = PlayerPrefs.GetFloat("PlayerX");
    float y = PlayerPrefs.GetFloat("PlayerY");
    int questId = PlayerPrefs.GetInt("QuestId");
    int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

    player.transform.position = new Vector3(x, y, 0);
    QM.questId = questId;
    QM.questActionIndex = questActionIndex;
    QM.ControlObject();
  }

  //게임 종료하기
  public void GameExit()
  {
    Application.Quit();
  }
}
