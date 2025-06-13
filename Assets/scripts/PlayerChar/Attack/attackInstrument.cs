using UnityEngine;
using UnityEngine.SceneManagement;

public class attackInstrument : MonoBehaviour
{
    public float despawn_time = 5.0f;
    public string minigameScene = "Minigame";

    public deployAttackInstrument deployScript;

    void Start()
    {
        Destroy(gameObject, despawn_time);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(minigameScene, LoadSceneMode.Additive);
        }
    }

    void OnDestroy()
    {
        deployScript.OnInstrumentDestroyed();
    }
}