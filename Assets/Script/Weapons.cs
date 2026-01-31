using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    #region Public Variables
    
    [Header("Weapons")]
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject currentWeapon;
    public Animator animator;
    
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
                animator.Play("SwordSwing");
                
                break;
        }
    }

    public void ChangeMask(playerMask mask)
    {
        
    }

    public void WeaponsHit()
    {
        currentWeapon.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void WeaponsNoHit()
    {
        currentWeapon.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void AnimationEnd()
    {
        gm.isAttacking = false;
    }
    
    #endregion
}
