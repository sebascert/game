using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    private bool startPlaying;
    public BeatScroller BS;
    public AudioSource music;
    private bool musicStarted = false;

    public static MinigameManager instance;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    private int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public Text scoreText;
    public Text comboText;
    public float currentCombo = 1f;

    private float notesPressed;
    public float totalNotes;

    void Start()
    {
        instance = this;
        notesPressed = 0f;
    }

    void Update()
    {
        if(!startPlaying && Input.anyKeyDown)
        {
            startPlaying = true;         
            StartCoroutine(StartAfterDelay());  
        } else if(startPlaying && musicStarted && !music.isPlaying)
        {
            ExitMinigame(); 
        }
    }

    public void NoteHit(Vector3 position)
    {
        currentCombo += .5f;
        
        float distance = Mathf.Abs(position.y);
        if (distance < 0.15f)
        {
            PerfectHit(position);
            startPlaying = true;
        }
        else if (distance < 0.25f)
        {
            GoodHit(position);
        }
        else
        {
            NomralHit(position);
        }
        
        scoreText.text = "Score: " + currentScore;
        comboText.text = "Combo: " + currentCombo.ToString("F1");

        notesPressed++;
    }

    private IEnumerator StartAfterDelay()
    {
        BS.hasStarted = true;
        yield return new WaitForSeconds(1.5f);
        music.Play();
        musicStarted = true;
    }

    public void NomralHit(Vector3 pos)
    {
        Debug.Log("Normal Hit!");
        Instantiate(hitEffect, pos, Quaternion.identity);
        currentScore += Mathf.RoundToInt(scorePerNote * currentCombo);
    }

    public void GoodHit(Vector3 pos)
    {
        Debug.Log("Good Hit!");
        Instantiate(goodEffect, pos, Quaternion.identity);
        currentScore += Mathf.RoundToInt(scorePerGoodNote * currentCombo);
    }

    public void PerfectHit(Vector3 pos)
    {
        Debug.Log("Perfect Hit!");
        Instantiate(perfectEffect, pos, Quaternion.identity);
        currentScore += Mathf.RoundToInt(scorePerPerfectNote * currentCombo);
    }

    public void NoteMissed(Vector3 pos)
    {
        Debug.Log("miss");
        Instantiate(missEffect, pos, Quaternion.identity);
        currentCombo = 1f;
        comboText.text = "Combo: x" + currentCombo.ToString("F1");
    }

    public void ExitMinigame()
    {
        float accuracy = Mathf.Clamp01(notesPressed / totalNotes);
        float damage = currentCombo * accuracy; //currentCombo * (totalNotes / notesPressed);
        float baseDamage = (currentScore/2)/100;
        MinigameResult.totalDamage = damage * baseDamage;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<Movement>().isFrozen = false;
            player.GetComponent<Health>().isInvincible = false;
        }

        SceneManager.UnloadSceneAsync("Minigame");
    }
}
