using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public int maxHealth = 8;
    private int currentHits;

    public Slider healthSlider;
    public bool isInvincible = false;
    public float invulnerabilityFrames;

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
        StartCoroutine(startInvFrames());
        UpdateHealthBar();
        if (currentHits >= maxHealth)
        {
            Die();
        }
    }

    public void RecoverHealth(int amount)
    {
        currentHits -= amount;
        if(currentHits <=0) //prevent negative hit mark
        {
            currentHits = 0;
        }
        UpdateHealthBar();
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
        // change to game over scene
    }

    public IEnumerator startInvFrames()
    {   
        Debug.Log("Enter Inv Frames");
        isInvincible = true;
        yield return new WaitForSeconds(invulnerabilityFrames);
        isInvincible = false;
        Debug.Log("Exit Inv Frames");
    }
}