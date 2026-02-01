using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class EnemyDamage : MonoBehaviour
{
    EnemyStats stats;
    EnemyMovement movement;
    EnemyBrain brain;

    public float selfKnockbackForce = 3f;
    public float contactCooldown = 0.2f;

    float lastHitTime;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        movement = GetComponent<EnemyMovement>();
        brain = GetComponent<EnemyBrain>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (Time.time - lastHitTime < contactCooldown)
            return;

        IDamageable dmg = collision.collider.GetComponent<IDamageable>();
        if (dmg == null) return;

        dmg.TakeDamage(stats.damage);
        lastHitTime = Time.time;

        Vector2 knockDir = (transform.position - collision.transform.position).normalized;

        movement.ApplyKnockback(knockDir * selfKnockbackForce);
    }
}
