using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Variables

    [Header("Masks")] 
    public Sprite[] masks;
    
    #endregion
    
    #region Private Variables

    private GameManager gm;
    
    #endregion
    
    #region Setup

    void Start()
    {
        gm = GameManager.Instance;
        gm.player = gameObject;
        gm.playerRB = GetComponent<Rigidbody2D>();
        gm.playerScript = this;
    }

    public void ChangeMask(playerMask newMask)
    {
        GetComponent<SpriteRenderer>().sprite = masks[(int)newMask];
    }
    
    #endregion
    
    
}
