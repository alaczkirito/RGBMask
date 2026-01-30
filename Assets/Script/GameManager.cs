using System;
using UnityEngine;

public enum playerMask
{
    Red,
    Green,
    Blue
}

public class GameManager : MonoBehaviour
{
    [Header("Player")] 
    public GameObject player;
    public float CurrentHP;
    public float MaxHP;
    public float CurrentMana;
    public float MaxMana;
    public float CurrentStamina;
    public float MaxStamina;
    public bool canDash = true;
    public bool isDashing = false;
    public float dashForce;
    public float moveSpeed;
    public Vector2 playerDirection = new Vector2(0,0);
    public playerMask currentMask = playerMask.Green;
    public bool isAttacking = false;


}
