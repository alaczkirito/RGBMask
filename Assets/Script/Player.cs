using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Variables

    [Header("Masks")] 
    public Sprite[] masks;
    public Sprite[] backgrounds;
    public SpriteRenderer maskRender;
    public SpriteRenderer bgRender;
    

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
        GameManager.OnChangeMask += ChangeMask;
    }

    public void ChangeMask(playerMask newMask)
    {
        maskRender.sprite = masks[(int)newMask];
        bgRender.sprite = backgrounds[(int)newMask];
    }
    
    #endregion
    
    
}
