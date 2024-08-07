using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Scene Transition Tile", menuName = "Tiles/Change Scene Tile")]
public class ChangeSceneTile : Tile
{
  public string targetSceneName; // 전환할 씬의 이름

}
