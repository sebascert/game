using UnityEngine;
using System.Collections;

public class AttackDamage : MonoBehaviour
{
    private float damageAmount;
    private float despawnTime = .5f;
    private float scaleGrowthTime = .1f;
    private Vector3 scaleGrowth;

    void Start()
    {
        scaleGrowth = new Vector3(.1f,.1f,0f);
        StartCoroutine(growScale());
    }

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
                Destroy(gameObject, despawnTime - .5f);
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        damageAmount = damage;
    }

    private IEnumerator growScale()
    {
        yield return new WaitForSeconds(scaleGrowthTime);


    }
}
