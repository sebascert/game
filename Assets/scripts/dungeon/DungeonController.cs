using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Cinemachine;
using Unity.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DungeonController : MonoBehaviour
{
    [SerializeField]
    private List<DungeonDoor> dungeonDoors;
    
    [SerializeField]
    private List<EnemySpawner> spawners;
    
    public UnityEvent onClearDungeon;
    public UnityEvent onStartDungeon;
    
    // must be a single cinemachine virtual cam
    private CinemachineConfiner2D cameraConfiner;
    private PolygonCollider2D dungeonConfiner;

    [SerializeField] 
    private bool cleared;
    [HideInInspector]
    public int completedSpawners;
    [HideInInspector]
    public List<GameObject> enemies;

    void Awake()
    {
        cleared = false;
        
        onStartDungeon.AddListener(() =>
        {
            if (cleared)
                return;
            StartCoroutine(EnterDungeon());
        });
        
        if (dungeonDoors.Count == 0)
            Debug.LogError("dungeon with no doors ");
        if (spawners.Count == 0)
            Debug.LogError("dungeon with no enemie spawners");
        
        dungeonDoors.ForEach((door) => door.controller = this);
    }

    private void Start()
    {
        cameraConfiner = FindFirstObjectByType<CinemachineConfiner2D>();
        if (!cameraConfiner)
            Debug.LogError("no CinemachineConfiner2D found");
        dungeonConfiner= GetComponent<PolygonCollider2D>();
        if (!dungeonConfiner)
            Debug.LogError("no dungeon confiner found");
    }

    private IEnumerator EnterDungeon()
    {
        dungeonDoors.ForEach((door) => door.Close());
        
        cameraConfiner.BoundingShape2D = dungeonConfiner;
        
        completedSpawners = 0;
        
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        spawners.ForEach((spawner) => StartCoroutine(spawner.StartSpawner(pos, this)));

        yield return new WaitUntil(() => completedSpawners == spawners.Count);
        yield return new WaitUntil(() => enemies.TrueForAll((enemy) => !enemy));
        
        enemies.Clear();
        onClearDungeon.Invoke();
        cameraConfiner.BoundingShape2D = null;

        dungeonDoors.ForEach((door) => door.Open());

        cleared = true;
    }
}
