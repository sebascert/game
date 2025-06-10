using UnityEngine;

public class MinigameResult : MonoBehaviour
{
    public static float totalDamage = 0f;
    public GameObject attackPrefab;  
    public Transform playerTransform;

    void Update()
    {
        if(totalDamage > 0f)
        {
            SpawnCircle();
            totalDamage = 0f;
            
            GameObject player = GameObject.FindWithTag("Player");
            StartCoroutine(player.GetComponent<Health>().startInvFrames());
        }
    }

    void SpawnCircle(){
        if (attackPrefab != null && playerTransform != null)
        {
            GameObject attack = Instantiate(attackPrefab, playerTransform.position, Quaternion.identity);
            AttackDamage attackScript = attack.GetComponent<AttackDamage>();

            if (attackScript != null)
            {
                attackScript.ApplyDamage(totalDamage);  
            }
        }
    }
}