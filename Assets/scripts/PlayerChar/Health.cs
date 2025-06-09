using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 8;
    private int currentHits;

    public Slider healthSlider;
    public bool isInvincible = false;


    void Start()
    {
        currentHits = 0;
        UpdateHealthBar();
    }

    public void TakeDamage( )
    {
        Movement movement = GetComponent<Movement>();
        if ((movement != null && movement.isDashing) || isInvincible) 
            return;

        currentHits ++;
        UpdateHealthBar();
        if (currentHits >= maxHealth)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
            healthSlider.value = (float)(maxHealth - currentHits) / maxHealth;
    }

    void Die()
    {
        Destroy(gameObject);
        if (healthSlider != null)
            healthSlider.gameObject.SetActive(false);
    }
}