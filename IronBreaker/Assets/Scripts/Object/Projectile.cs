using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발사체(자동 삭제)
public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed;
  [SerializeField] private Rigidbody2D rigid;
  [SerializeField] private float lifeTime;

  void Start()
  {
    rigid.velocity = transform.right * speed;
    Invoke("DestroyProjectTile", lifeTime);
  }

  public void OnFire(Vector3 vec)
  {
    rigid.velocity = vec * speed;
    Invoke("DestroyProjectTile", lifeTime);
  }

  void DestroyProjectTile()
  {
    Destroy(gameObject);
  }
}
