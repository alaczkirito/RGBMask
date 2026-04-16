using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    
    #region Public Variables
    
        
    
    #endregion
    
    #region Private Variables

        private InputAction movementKey;
        private InputAction dashKey;
        private Vector2 inputVector;
        private float dashTimer;
    
    #endregion
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        speed = 10;

        movementKey = InputSystem.actions.FindAction("Move");
        dashKey = InputSystem.actions.FindAction("Sprint");
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (movementKey.IsPressed()) inputVector = movementKey.ReadValue<Vector2>();
        else inputVector = Vector2.zero;
        Move(speed, inputVector);
        
        if (dashKey.WasPressedThisFrame()) Dash();
        if (dashTimer > 0) dashTimer -= Time.deltaTime;
        if (dashTimer <= 0 && speed > 10) speed -= 10;
    }

    private void Dash()
    {
        dashTimer = 0.2f;
        speed = 100;
    }
    
}
