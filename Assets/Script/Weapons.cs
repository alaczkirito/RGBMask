using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class initialTransform
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;
}

public class Weapons : MonoBehaviour
{
    #region Public Variables
    
    [Header("Weapons")]
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject currentWeapon;
    public Animator animator;
    public BoxCollider2D weaponHitBox;
    
    public List<initialTransform> presets = new List<initialTransform>();

    
    #endregion
    
    #region Private Variables

    private GameManager gm;
    private bool isStaffLeft;
    
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
        
        ApplyPreset(0);
    }
    
    private void ApplyPreset(int presetID)
    {
        var preset = presets[presetID];
        if (preset == null) return;

        transform.position = preset.position;
        transform.rotation = Quaternion.Euler(preset.rotation);
        transform.localScale = preset.scale;
    }

    public void PlayAnimation()
    {
        switch (gm.currentMask)
        {
            // case playerMask.Red:
            //
            //     break;
            case playerMask.Blue:
                if (isStaffLeft)
                {
                    animator.Play("StaffToRight", 0 , 0f);
                    isStaffLeft = false;
                }
                else
                {
                    animator.Play("StaffToLeft",0, 0f);
                    isStaffLeft = true;
                }
                break;
            case playerMask.Green:
                animator.Play("SwordSwing", 0, 0f);
                break;
        }
    }

    public void ChangeMask(playerMask mask)
    {
        currentWeapon = weapons[(int)mask];
        foreach (GameObject weapon in weapons)
        {
            if(weapon != currentWeapon) weapon.SetActive(false);
            else weapon.SetActive(true);
        }
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
