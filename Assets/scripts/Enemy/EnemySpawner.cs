using System;

using UnityEngine;
using System.Collections;

[Serializable]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int count;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float spawnRadius;

    private void Start()
    {
        if (!prefab) 
            Debug.LogError("missing enemy prefab");
    }

    public IEnumerator StartSpawner(Vector3 position, DungeonController dungeon)
    {
        yield return new WaitForSeconds(startDelay);
        
        for (int i = 0; i < count; i++)
        {
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = (Vector2)position + randomOffset;
            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            dungeon.enemies.Add(enemy);
            
            if (i < count - 1)
                break;
            yield return new WaitForSeconds(delay);
        }

        dungeon.completedSpawners++;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}