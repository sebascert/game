using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class MinigameManager : MonoBehaviourSingleton<MinigameManager>
{
    private bool startPlaying;
    public BeatScroller BS;
    public AudioSource music;
    private bool musicStarted = false;

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

    public float noteCollisionDist;
    public ButtonController[] buttons;

    void Start()
    {
        notesPressed = 0f;
        startPlaying = true;         
        StartCoroutine(StartAfterDelay());  
    }

    void Update()
    {
        if(startPlaying && musicStarted && !music.isPlaying)
            ExitMinigame(); 
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
        Time.timeScale = 0;
        BS.hasStarted = true;
        yield return new WaitForSecondsRealtime(1.5f);
        music.Play();
        musicStarted = true;
    }

    public void NomralHit(Vector3 pos)
    {
        Instantiate(hitEffect, pos, Quaternion.identity);
        currentScore += Mathf.RoundToInt(scorePerNote * currentCombo);
    }

    public void GoodHit(Vector3 pos)
    {
        Instantiate(goodEffect, pos, Quaternion.identity);
        currentScore += Mathf.RoundToInt(scorePerGoodNote * currentCombo);
    }

    public void PerfectHit(Vector3 pos)
    {
        Instantiate(perfectEffect, pos, Quaternion.identity);
        currentScore += Mathf.RoundToInt(scorePerPerfectNote * currentCombo);
    }

    public void NoteMissed(Vector3 pos)
    {
        Instantiate(missEffect, pos, Quaternion.identity);
        currentCombo = 1f;
        comboText.text = "Combo: x" + currentCombo.ToString("F1");
    }

    public void ExitMinigame()
    {
        Time.timeScale = 1;
        float accuracy = Mathf.Clamp01(notesPressed / totalNotes);
        float damage = currentCombo * accuracy; //currentCombo * (totalNotes / notesPressed);
        float baseDamage = (currentScore/2)/100;
        MinigameResult.totalDamage = damage * baseDamage;

        SceneManager.UnloadSceneAsync("Minigame");
    }

    public bool IsNoteColliding(NoteObject note)
    {
        foreach (ButtonController button in buttons)
        {
            // buttons and notes must be perfectly synced
            if (button.transform.position.x != note.transform.position.x)
                continue;
            if (Mathf.Abs(button.transform.position.y - note.transform.position.y) > noteCollisionDist)
                continue;
            return true;
        }

        return false;
    }
}
