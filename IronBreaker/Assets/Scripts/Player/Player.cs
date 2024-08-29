using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public static Player instance;

  public PlayerStats playerStats; // ScriptableObject 참조

  public float MaxHP { get { return playerStats.maxHP; } }
  public float CurrentHP { get; private set; }
  public float MaxStamina { get { return playerStats.maxStamina; } }
  public float CurrentStamina { get; private set; }
  public float MaxMagneticPower { get { return playerStats.maxMP; } }
  public float CurrentMagneticPower { get; private set; }
  public float Defense { get { return playerStats.defense; } }
  public float MoveSpeed { get { return playerStats.moveSpeed; } }

  private bool isInvincible = false; // 무적 상태 여부

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject); // 이 오브젝트를 씬 전환 시 파괴되지 않도록 설정
    }
    else if (instance != this)
    {
      Destroy(gameObject); // 중복 생성 방지
    }
    InitializeStats();
  }

  // 스탯 초기화
  void InitializeStats()
  {
    CurrentHP = MaxHP;
    CurrentStamina = MaxStamina;
    CurrentMagneticPower = MaxMagneticPower;
  }

  // 무적 상태 설정 메서드
  public void SetInvincible(bool value)
  {
    isInvincible = value;
    // 무적 상태 시각적 표시 추가 가능
  }

  // 무적 상태 확인 메서드
  public bool IsInvincible()
  {
    return isInvincible;
  }

  // 기타 플레이어 관련 메서드...
}
