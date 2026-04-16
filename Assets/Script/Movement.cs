using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Public Variables
    
        public float speed;
        public Vector2 position;
        public Vector2 direction;
        public Rigidbody2D rbRead => rb;
        
    #endregion

    #region  Protected Variables

    protected Rigidbody2D rb;

    #endregion
    
    #region Private Variables
    
        
    
    #endregion
    
    #region Main Functions
    
        protected virtual void Start()
        {
            rb = (TryGetComponent<Rigidbody2D>(out Rigidbody2D TryGetrb)) ? TryGetrb : throw new MissingComponentException("Rigidbody2D not found on " + gameObject.name); 
        }
        

        public void Move(float externalSpeed, Vector2 externalDirection)
        {
            speed = externalSpeed;
            direction = externalDirection;
            rb.linearVelocity = direction * speed;
        }
        
    #endregion
}
