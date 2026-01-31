using UnityEngine;

public class Damagables : MonoBehaviour
{
    public bool isPlayer;
    public bool isEnemy;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("Hit");
        }
    }
}
