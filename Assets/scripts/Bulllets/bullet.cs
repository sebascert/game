using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;


    private Vector2 spawnPoint;
    private float timer = 0f;


    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        Health playerHealth = collision.GetComponent<Health>();
        Movement playerMovement = collision.GetComponent<Movement>();
        if (playerHealth != null && collision.CompareTag("Player"))
        {
            if (!playerHealth.isInvincible && !playerMovement.isDashing)
            {
                playerHealth.TakeDamage();
                Destroy(gameObject);
            }
        }
    }

    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
}