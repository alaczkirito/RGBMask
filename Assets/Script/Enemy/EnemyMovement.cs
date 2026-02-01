using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyBrain brain;
    EnemyStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        brain = GetComponent<EnemyBrain>();
        stats = GetComponent<EnemyStats>();
    }

    private void FixedUpdate()
    {
        if (brain.currentState != EnemyStates.Chase)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Transform target = brain.Target;
        if (target != null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * stats.moveSpeed;
    }

    public void ApplyKnockback(Vector2 force)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

}
