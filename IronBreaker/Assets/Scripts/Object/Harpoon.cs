using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 고유무기 '작살' 
//해당 스크립트는 플레이어의 사격 포인트를 대신한다.
public class Harpoon : MonoBehaviour
{
  [SerializeField] private GameObject projectilePrefabs;
  [SerializeField] private Transform firePoint;

  //미개발 - 차징된 작살이 다르다면 사용할 예정 / 다르지않다면 사용X
  [SerializeField] private GameObject chargedProjectilePrefabs;

  void Update()
  {
    Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, z);
  }

  public void CreateProjectile()
  {
    Instantiate(projectilePrefabs, firePoint.position, firePoint.rotation);
  }
  public void CreateChargedProjectile()
  {
    Instantiate(chargedProjectilePrefabs, firePoint.position, firePoint.rotation);
  }
}
