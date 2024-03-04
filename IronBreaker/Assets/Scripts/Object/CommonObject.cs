using UnityEngine;

//오브젝트의 공통적인 기능
public class CommonObject : MonoBehaviour
{
  public Texture2D cursorCrosshair;
  public Texture2D cursorDialog;

  //기본값
  void Start()
  {
    //커서의 디폴트값을 크로스헤어로 한다. 씬에 따라 다를 가능성 있음.
    Cursor.SetCursor(cursorCrosshair, Vector2.zero, CursorMode.ForceSoftware);
  }

  //오브젝트에 마우스가 들어왔을때
  void OnMouseEnter()
  {
    Cursor.SetCursor(cursorDialog, Vector2.zero, CursorMode.ForceSoftware);
  }

  //오브젝트에서 마우스가 나갔을때
  void OnMouseExit()
  {
    Cursor.SetCursor(cursorCrosshair, Vector2.zero, CursorMode.ForceSoftware);
  }
}
