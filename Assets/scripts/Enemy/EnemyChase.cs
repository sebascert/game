using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private GameObject playerTransform;
    public float speed;
    public int range;
    public int closure;

    private float distance;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if (closure < distance && distance < range)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.transform.position,
                    speed * Time.deltaTime
                );
            }
        } else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }
    }
}
