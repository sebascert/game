using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    private float damageAmount;
    private float despawnTime = .5f;

    void Update()
    {
        Destroy(gameObject, despawnTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);  
                Destroy(gameObject, despawnTime - .3f);
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        damageAmount = damage;
    }
}
