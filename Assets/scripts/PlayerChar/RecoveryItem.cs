using UnityEngine;
using System.Collections;

public class RecoveryItem : MonoBehaviour
{
    [SerializeField] private int healthToRecover = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().RecoverHealth(healthToRecover);
            Destroy(gameObject);
        }
    }
}