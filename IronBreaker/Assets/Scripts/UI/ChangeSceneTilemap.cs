using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ChangeSceneTilemap : MonoBehaviour
{
  public Tilemap tilemap; // 씬 전환을 처리할 Tilemap
  public TileBase transitionTile; // 씬 전환에 사용될 특정 Tile
  public string targetSceneName; // 전환할 씬의 이름

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      SceneManager.LoadScene(targetSceneName);
    }
  }
}
