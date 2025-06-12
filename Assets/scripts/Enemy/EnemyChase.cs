using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform playerTransform;
    public float speed;
    public int range;
    public int closure;

    private float distance;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!playerTransform)
            Debug.LogError("EnemyChase unable to find player");
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, playerTransform.position);
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (closure < distance && distance < range)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerTransform.position,
                speed * Time.deltaTime
            );
        }
    }
}
