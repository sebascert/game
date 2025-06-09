using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bulletFire : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Burst}

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private int burstCount = 3;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance;
    [SerializeField] private float burstTime = 0.1f;
    [SerializeField] private bool stagger;
    
    private GameObject spawnedBullet;
    private float timer = 0f;
    private bool isShooting = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin)
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

        if (timer >= firingRate && !isShooting)
        {
            if (spawnerType == SpawnerType.Burst)
                StartCoroutine(BurstFire());
            else
                Fire();
            
            timer = 0;
        }
    }

    private void Fire(Vector2? directionOverride = null, Vector2? spawnPosition = null)
    {
        if (bullet)
        {
            Quaternion bulletRotation = transform.rotation; 
            Vector2 direction = transform.right;            

            if (spawnerType == SpawnerType.Burst || spawnerType == SpawnerType.Straight)
            {
                if (directionOverride != null)
                {
                    direction = directionOverride.Value.normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    bulletRotation = Quaternion.Euler(0f, 0f, angle);
                }
                else
                {
                    GameObject player = GameObject.FindWithTag("Player");
                    if (player != null)
                    {
                        direction = (player.transform.position - transform.position).normalized;
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        bulletRotation = Quaternion.Euler(0f, 0f, angle);
                    }
                }
            }

            Vector3 spawnPos = spawnPosition.HasValue ? spawnPosition.Value : transform.position;
            spawnedBullet = Instantiate(bullet, spawnPos, bulletRotation);

            Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();
            bulletScript.speed = speed;
            bulletScript.bulletLife = bulletLife;
        }
    }

    private IEnumerator BurstFire()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) yield break;
        
        isShooting = true;

        Vector2 toPlayer = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        float currentAngle = targetAngle;
        float angleStep = 0f;
        float startAngle = targetAngle;

        if(angleSpread != 0){
            angleStep = angleSpread / (projectilesPerBurst - 1);
            startAngle = targetAngle - angleSpread / 2f;
            currentAngle = startAngle;
        }

        float timeBetweenProjectiles = 0f;
        if (stagger){
            timeBetweenProjectiles = burstTime / projectilesPerBurst;
        }

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 spawnPos = FindBulletSpawnPos(currentAngle);
                Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                Fire(direction, spawnPos);
                currentAngle += angleStep;

                if (stagger){
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }
            currentAngle = startAngle;
            yield return new WaitForSeconds(burstTime);
        }
        yield return new WaitForSeconds(firingRate);
        isShooting = false;
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x,y);

        return pos;
    }
}
