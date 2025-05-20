using System;

using UnityEngine;

class GameStateSystem : GameSystem<GameStateSystem>
{
    public GameState State { get; private set; } = GameState.Start;

    private GameID _gameID;
    public GameID GameID
    {
        get => _gameID;
        set
        {
            if (State != GameState.MainMenu)
                Debug.LogError("GameManager: attempting to modify GameID on invalid state");
            _gameID = value;
        }
    }

    // game info
    public int Level { get; private set; }

    public override void Init()
    {
        if (State != GameState.Start)
            throw new Exception("GameStateSystem: invalid initial GameState");
    }
}

enum GameState
{
    Start,
    MainMenu,
    Pause,
    Cinematic,
    OnLevel,
    OnDungeon,
}