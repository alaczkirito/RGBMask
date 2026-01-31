using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    private GameManager gm;
    
    #endregion
    
    #region Setup

    void Start()
    {
        gm = GameManager.Instance;
        gm.player = gameObject;
        gm.playerRB = GetComponent<Rigidbody2D>();
    }
    
    #endregion
    
    
}
