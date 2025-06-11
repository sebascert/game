using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalEnemy;
    [SerializeField] private GameObject eliteEnemy;

    [SerializeField] private int numberOfNormalEnemy;
    [SerializeField] private int numberOfEliteEnemy;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float spawnRadius = 3f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(spawnEnemy(eliteEnemy, numberOfEliteEnemy));
            StartCoroutine(spawnEnemy(normalEnemy, numberOfNormalEnemy));
            Destroy(gameObject);
        }
    }

    private IEnumerator spawnEnemy(GameObject enemy, int numberOfEnemy)
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = (Vector2)transform.position + randomOffset;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
}
