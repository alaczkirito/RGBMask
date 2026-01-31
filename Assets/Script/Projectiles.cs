using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public bool fromPlayer;
    public bool fromEnemy;
    public void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Animator>().Play("Projectile");
    }

    // private void MageBulletHit()
    // {
    //     
    // }
}
