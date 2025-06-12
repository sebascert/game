using System;

using UnityEngine;
using UnityEngine.Serialization;

public class DungeonDoor : MonoBehaviour
{
    private enum EnterSide { Top, Bottom, Left, Right }
    
    [SerializeField]
    private EnterSide enterSide;
    
    [HideInInspector]
    public DungeonController controller;
    
    private Collider2D collider;
    //private SpriteRenderer SR;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        if (!controller)
            Debug.LogError("missing reference to DungeonController");
        /*
        SR = GetComponent<SpriteRenderer>();
        if (!SR)
            Debug.LogError("missing reference to SpriteREnderer");*/
        
        collider.isTrigger = true;
        //SR.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        Vector3 playerPos = other.transform.position;
        switch (enterSide)
        {
            case EnterSide.Top:
                if (playerPos.y < transform.position.y)
                    return;
                break;
            case EnterSide.Bottom:
                if (playerPos.y > transform.position.y)
                    return;
                break;
            case EnterSide.Left:
                if (playerPos.x > transform.position.x)
                    return;
                break;
            case EnterSide.Right:
                if (playerPos.x < transform.position.x)
                    return;
                break;
        }
        controller.onStartDungeon.Invoke();
    }

    public void Close()
    {
        collider.isTrigger = false;
        //SR.enabled = true;
        // missing door sprite
    }

    public void Open()
    {
        collider.enabled = false;
        //SR.enabled = false;
    }
}
