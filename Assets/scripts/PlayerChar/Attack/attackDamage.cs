using UnityEngine;
using System.Collections;

public class AttackDamage : MonoBehaviour
{
    private float damageAmount;
    [SerializeField] private float despawnTime = 1f;
    [SerializeField] private float scaleGrowthTime = .01f;
    [SerializeField] private float x,y;
    private Vector3 scaleGrowth;

    void Start()
    {
        scaleGrowth = new Vector3(x, y, 0f);
        StartCoroutine(growScale());
        Destroy(gameObject, despawnTime);
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
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        damageAmount = damage;
    }

    private IEnumerator growScale()
    {
        while(true)
        {
            yield return new WaitForSeconds(scaleGrowthTime);
            transform.localScale += scaleGrowth;
        }
    }
}
