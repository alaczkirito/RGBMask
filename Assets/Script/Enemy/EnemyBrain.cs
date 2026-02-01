using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public EnemyStates currentState;

    EnemyMovement movement;
    Transform target;

    public float detectionRange = 6f;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (currentState == EnemyStates.Dead)
            return;
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            detectionRange,
            LayerMask.GetMask("Player")
            );

        target = hit ? hit.transform : null;
    }

    void HandleState()
    {
        if (target != null)
        {
            currentState = EnemyStates.Chase;
        }
        else
        {
            currentState = EnemyStates.Patrol;
        }
    }
    public Transform Target => target;
}
