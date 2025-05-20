using System;

using UnityEngine;

class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    private GameSystem[] gameSystems;

    private void Start()
    {
        //custom Start for game systems, as their singleton property is asserted in Awake
        foreach (GameSystem gameSystem in gameSystems)
            gameSystem.Init();
    }
}