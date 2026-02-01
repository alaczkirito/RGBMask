using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHP = 30;
    public float moveSpeed = 2.5f;
    public float damage = 8f;

    float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    void Die()
    {
        GetComponent<EnemyBrain>().currentState = EnemyStates.Dead;
        Destroy(gameObject);
    }
}
