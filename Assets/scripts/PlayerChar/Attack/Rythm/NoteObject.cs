using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool hasBeenHit = false;
    public KeyCode keyToPress;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed && !hasBeenHit)
            {
                hasBeenHit = true;
                gameObject.SetActive(false);

                MinigameManager.Instance.NoteHit(transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator" && !hasBeenHit)
        {
            canBePressed = false;
            MinigameManager.Instance.NoteMissed(transform.position);
        }
    }
}