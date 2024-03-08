using UnityEngine;


//이 스크립트는 "플레이어의 데이터"와 관련된 내용이 들어갑니다.
public class Player : MonoBehaviour
{
  public float MaxHP { get { return maxHP; } }    //최대 체력
  public float CurrentHP { get { return currentHP; } }    //현재 체력
  public float MaxStamina { get { return maxStamina; } }    //최대 스태미나
  public float CurrentStamina { get { return currentStamina; } }    //현재 스태미나
  public float MaxMagneticPower { get { return maxMP; } }//maxMagneticPower의 약
  public float CurrentMagneticPower { get { return currentMP; } } //현재 마그네틱파워
  public float Defense { get { return defense; } }    //방어력
  public float MoveSpeed { get { return moveSpeed; } } //테스트용으로했을때 1초에 4개타일을 움직이려면 3.5~4이내의 값이 필요했음.


  [Header("플레이어 스테이터스")]
  [SerializeField] protected float maxHP;
  [SerializeField] protected float currentHP;
  [SerializeField] protected float maxStamina;
  [SerializeField] protected float currentStamina;
  [SerializeField] protected float maxMP; 
  [SerializeField] protected float currentMP;
  [SerializeField] protected float defense;
  [SerializeField] protected float moveSpeed;

  //stat init
  public void OnUpdateStat(float maxHP, float currentHP, float maxStamina, float currentStamina, float maxMP, float currentMP, float defense, float moveSpeed)
  {
    this.maxHP = maxHP;
    this.currentHP = currentHP;
    this.maxStamina = maxStamina;
    this.currentStamina = currentStamina;
    this.maxMP = maxMP;
    this.currentMP = currentMP;
    this.defense = defense;
    this.moveSpeed = moveSpeed;
  }


}
