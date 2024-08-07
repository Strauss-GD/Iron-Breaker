using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//작살형태의 Sprite를 추가할때까지 발사체로 명명
public class Harpoon : MonoBehaviour
{
  [SerializeField] private GameObject projectilePrefabs;
  [SerializeField] private Transform firePoint;

  // 차징된 발사체 프리팹
  [SerializeField] private GameObject chargedProjectilePrefabs;

  void Update()
  {
    // 마우스 방향을 계산하여 회전
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = (mousePos - transform.position).normalized;
    float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, z);
  }

  public void CreateProjectile()
  {
    Instantiate(projectilePrefabs, firePoint.position, firePoint.rotation);
  }

  public void CreateChargedProjectile()
  {
    if (chargedProjectilePrefabs != null)
    {
      Instantiate(chargedProjectilePrefabs, firePoint.position, firePoint.rotation);
    }
    else
    {
      Debug.LogWarning("프리팹 null.");
    }
  }
}