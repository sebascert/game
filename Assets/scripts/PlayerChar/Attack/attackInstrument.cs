using UnityEngine;
using UnityEngine.SceneManagement;

public class attackInstrument : MonoBehaviour
{
    public float despawn_time = 5.0f;
    public string minigameScene = "Minigame";

    public deployAttackInstrument deployScript;
    public delegate void InstrumentDestroyed();
    public event InstrumentDestroyed OnDestroyed;

    void Start()
    {
        /*if (MinigameResult.totalDamage > 0f)
        {
            EnemyHealth enemy = FindFirstObjectByType<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(MinigameResult.totalDamage);
            }

            MinigameResult.totalDamage = 0f;
        }*/

        Destroy(gameObject, despawn_time);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Minigame by: " + collision.gameObject.name);
            
            var movement = collision.GetComponent<Movement>();
            var health = collision.GetComponent<Health>();
            if (movement != null) movement.isFrozen = true;
            if (health != null) health.isInvincible = true;

            SceneManager.LoadScene(minigameScene, LoadSceneMode.Additive);
        }
    }

    void OnDestroy()
    {
        OnDestroyed?.Invoke();

        if (deployScript != null)
        {
            deployScript.OnInstrumentDestroyed();
        }
    }
}
