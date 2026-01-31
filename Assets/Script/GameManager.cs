using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum playerMask
{
    Red,
    Green,
    Blue
}

public class GameManager : MonoBehaviour
{
    #region Public Variables
    
    [Header("Game Manager")]
    public static GameManager Instance { get; private set; }
    
    [Header("Player")] 
    public GameObject player;
    public Rigidbody2D playerRB;
    public float CurrentHP;
    public float MaxHP;
    public float CurrentMana;
    public float MaxMana;
    public float CurrentStamina;
    public float MaxStamina;
    public bool canDash = true;
    public bool isDashing = false;
    public bool longDash = false;
    public float dashForce;
    public bool isRunning = false;
    public bool canRun = false;
    public float runSpeed;
    public float moveSpeed;
    public Vector2 playerDirection = new Vector2(0,0);
    public playerMask currentMask = playerMask.Green;
    public bool isAttacking = false;
    public bool canAttack = true;
    public float attackDamage;
    
    #endregion

    #region Private Variables
    
    
    
    #endregion
    
    #region Setups

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed) Debug.Log(playerDirection);
        
        if (Keyboard.current.aKey.isPressed) playerDirection.x = -1;
        if (Keyboard.current.dKey.isPressed) playerDirection.x = 1;
        if (Keyboard.current.wKey.isPressed) playerDirection.y = 1;
        if (Keyboard.current.sKey.isPressed) playerDirection.y = -1;

        
        playerDirection.Normalize();
        
        playerRB.linearVelocity = playerDirection * moveSpeed;
    }
    
    #endregion
}
