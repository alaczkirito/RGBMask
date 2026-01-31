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
    public Player playerScript;
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
    public Vector2 dashVelocity = Vector2.zero;
    public float dashDuration = 0.10f;
    public bool isRunning = false;
    public bool canRun = false;
    public float runSpeed;
    public float moveSpeed;
    public Vector2 playerDirection = new Vector2(0,0);
    public Vector2 lastDirection = new Vector2(0,0);
    public playerMask currentMask = playerMask.Green;
    public bool isAttacking = false;
    public bool canAttack = true;
    public float attackDamage;
    
    #endregion

    #region Private Variables
    
    private Vector2 lastVelocity = Vector2.zero;
    
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
        var kb = Keyboard.current;
        if (kb == null) return;
        
        // if (kb.anyKey.isPressed) Debug.Log(playerDirection);
        
        if (kb.aKey.isPressed) playerDirection.x = -1;
        else if (kb.dKey.isPressed) playerDirection.x = 1;
        else playerDirection.x = 0;
        
        if (kb.wKey.isPressed) playerDirection.y = 1;
        else if (kb.sKey.isPressed) playerDirection.y = -1;
        else playerDirection.y = 0;
        
        playerDirection.Normalize();
        
        if (playerDirection.x != 0 || playerDirection.y != 0) lastDirection = playerDirection;

        if (kb.shiftKey.wasPressedThisFrame && canDash)
        {
            Vector2 dashDirection = playerDirection != Vector2.zero ? playerDirection : lastDirection;
            
            dashVelocity = dashDirection.normalized * dashForce;
            isDashing = true;
        }
        
        if(kb.digit1Key.wasPressedThisFrame) playerScript.ChangeMask(playerMask.Red);
        if(kb.digit2Key.wasPressedThisFrame) playerScript.ChangeMask(playerMask.Green);
        if(kb.digit3Key.wasPressedThisFrame) playerScript.ChangeMask(playerMask.Blue);
        
        
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = playerDirection * moveSpeed;

        // If we have a dashVelocity, decay it towards zero over dashDuration seconds.
        if (dashVelocity != Vector2.zero && dashDuration > 0f)
        {
            // compute how much magnitude to remove this fixed step so that dash becomes zero after dashDuration
            float decayPerSecond = dashVelocity.magnitude / dashDuration;
            float decayThisStep = decayPerSecond * Time.fixedDeltaTime;

            dashVelocity = Vector2.MoveTowards(dashVelocity, Vector2.zero, decayThisStep);
        }
        else
        {
            // If dashDuration <= 0 treat as instant (no dash effect)
            if (dashDuration <= 0f) dashVelocity = Vector2.zero;
        }

        // final velocity = walking + dash component
        playerRB.linearVelocity = targetVelocity + dashVelocity;
    }

    void DashCoolDown()
    {
        
    }

    void ChangeMask(playerMask mask)
    {
        playerScript.ChangeMask(mask);
        
        //Stat change later
    }
    
    #endregion
}
