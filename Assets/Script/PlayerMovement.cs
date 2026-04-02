using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    
    #region Public Variables
    
        
    
    #endregion
    
    #region Private Variables

    private InputAction movementKey;
        private Vector2 inputVector;
        
        [SerializeField]
        private float playerSpeed = 10;
    
    #endregion
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    
        // var playerMap = asset.FindActionMap("Player");
        // playerMap.Enable();
    
        movementKey = InputSystem.actions.FindAction("Move");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movementKey.IsPressed()) inputVector = movementKey.ReadValue<Vector2>();
        Move(playerSpeed, inputVector);
    }
    
    
}
