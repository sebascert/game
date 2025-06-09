using UnityEngine;
using System.Collections;

public class BossAi : MonoBehaviour
{
    [Header("Attack Prefabs")]
    public GameObject staggerAttack;
    public GameObject burstAttack;
    public GameObject waveAttack;

    [Header("Attack Settings")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AttackCycle());
    }

    private IEnumerator AttackCycle()
    {
        while(true)
        {
            int randomValue = Random.Range(0, 3);
            GameObject selectedAttack = null;

            float xOffset = 0f;
            float yOffset = 0f;

            switch(randomValue)
            {
                case 0:
                    selectedAttack = staggerAttack;
                    xOffset = 2f;
                    yOffset = 1f;
                    break;
                case 1:
                    selectedAttack = burstAttack;
                    xOffset = 3.5f;
                    yOffset = 2.5f;
                    break;
                case 2:
                    selectedAttack = waveAttack;
                    xOffset = 1.5f;
                    yOffset = 3f;
                    break;    
            }

            if(selectedAttack != null)
            {
                Vector3 leftPos = transform.position + new Vector3(-xOffset, yOffset, 0);
                Vector3 rightPos = transform.position + new Vector3(xOffset, yOffset, 0);

                GameObject leftAttack = Instantiate(selectedAttack, leftPos, Quaternion.identity);
                GameObject rightAttack = Instantiate(selectedAttack, rightPos, Quaternion.identity);

                Destroy(leftAttack, attackDuration);
                Destroy(rightAttack, attackDuration);
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }
}
