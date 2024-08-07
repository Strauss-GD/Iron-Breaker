using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player/Player Stats")]
public class PlayerStats : ScriptableObject
{
  public float maxHP;
  public float maxStamina;
  public float maxMP;
  public float defense;
  public float moveSpeed;
}