using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 고유무기 '작살' 
//해당 스크립트는 플레이어의 사격 포인트를 대신한다.
public class Harpoon : MonoBehaviour
{
  [SerializeField] private GameObject projectile;
  [SerializeField] private Transform firePoint;

  public void CreateProjectile()
  {
    Instantiate(projectile, firePoint.position, firePoint.rotation);
  }
}
