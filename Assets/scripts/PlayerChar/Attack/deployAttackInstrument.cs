using UnityEngine;
using System.Collections;

public class deployAttackInstrument : MonoBehaviour
{
    public GameObject attackInstrumentPrefab;
    public float respawnTime = 4.0f;
    public Transform playerTransform;
    public float spawnRadius = 3f;
    public bool spawned = false;

    private GameObject currentInstrument;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private void SpawnInstrument()
    {
        if (playerTransform != null && currentInstrument == null)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = (Vector2)playerTransform.position + randomOffset;

            currentInstrument = Instantiate(attackInstrumentPrefab, spawnPosition, Quaternion.identity);

            attackInstrument instrumentScript = currentInstrument.GetComponent<attackInstrument>();
            if (instrumentScript != null)
            {
                instrumentScript.deployScript = this;  
            }

            spawned = true;
        } 
    }

    //Coroutine
    IEnumerator SpawnLoop()
    {
        var movement = playerTransform.GetComponent<Movement>();

        while (movement != null && movement.isFrozen)
        {
            yield return null; 
        }

        while(true){ 
            if(!spawned)
            {
                SpawnInstrument();
                spawned = true;
            }
            yield return new WaitForSeconds(respawnTime);
        }
    }

    public void OnInstrumentDestroyed()
    {
        spawned = false;
        currentInstrument = null;
    }
}
