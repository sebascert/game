using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool hasBeenHit = false;
    public KeyCode keyToPress;

    void Update()
    {
        if (!canBePressed && MinigameManager.Instance.IsNoteColliding(this))
            canBePressed = true;
        if (canBePressed && !MinigameManager.Instance.IsNoteColliding(this))
        {
            canBePressed = false;
            MinigameManager.Instance.NoteMissed(transform.position);
        }
        if (!hasBeenHit && Input.GetKeyDown(keyToPress) && canBePressed)
        {
            hasBeenHit = true;
            gameObject.SetActive(false);

            MinigameManager.Instance.NoteHit(transform.position);
        }
    }
}