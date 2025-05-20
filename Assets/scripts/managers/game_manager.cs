using System;

using UnityEngine;

class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    private GameSystemBase[] gameSystems;

    private void Start()
    {
        //custom Start for game systems, as their singleton property is asserted in Awake
        foreach (GameSystemBase gameSystem in gameSystems)
            gameSystem.Init();
    }
}