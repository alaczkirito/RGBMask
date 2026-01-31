using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    #region Public Variables
    
    [Header("Weapons")]
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject currentWeapon;
    public Animator animator;
    public BoxCollider2D weaponHitBox;
    
    #endregion
    
    #region Private Variables

    private GameManager gm;
    private bool isWandLeft;
    
    #endregion
    
    #region Setup
    
    void Start()
    {
        gm = GameManager.Instance;
        gm.weapons = gameObject;
        gm.weaponScript = this;
        GameManager.OnChangeMask += ChangeMask;
        
        animator = GetComponent<Animator>();
        
        //temp
        currentWeapon = weapons[0];
        weaponHitBox = currentWeapon.GetComponent<BoxCollider2D>();
        weaponHitBox.enabled = false; 
        
    }

    public void PlayAnimation()
    {
        switch (gm.currentMask)
        {
            case playerMask.Red:
        
                break;
            case playerMask.Blue:

                break;
            case playerMask.Green:
                animator.Play("SwordSwing", 0, 0f);
                break;
        }
    }

    public void ChangeMask(playerMask mask)
    {
        currentWeapon = weapons[(int)mask];
    }

    public void WeaponsHit()
    {
       weaponHitBox.enabled = true;
    }

    public void WeaponsNoHit()
    {
        weaponHitBox.enabled = false;
    }

    public void AnimationEnd()
    {
        gm.isAttacking = false;
    }
    
    #endregion
}
