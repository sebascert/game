using System;

class GameStateSystem : GameSystem<GameStateSystem>
{
    private GameState _prevState;
    private GameState _state = GameState.Start;
    public GameState State
    {
        get => _state;
        set
        {
            if (value == GameState.Start)
                goto INVALID_STATE_TRANSITION;
            switch (_state)
            {
                case GameState.Start:
                    if (value != GameState.MainMenu)
                        goto INVALID_STATE_TRANSITION;
                    break;
                case GameState.MainMenu:
                    if (value != GameState.OnLevel)
                        goto INVALID_STATE_TRANSITION;
                    break;
                case GameState.OnDungeon:
                    if (value != GameState.OnLevel && value != GameState.Pause)
                        goto INVALID_STATE_TRANSITION;
                    break;
                case GameState.Pause:
                    if (value != _prevState)
                        goto INVALID_STATE_TRANSITION;
                    break;
                case GameState.OnLevel:
                    if (value != GameState.OnDungeon && value != GameState.Pause && value != GameState.MainMenu)
                        goto INVALID_STATE_TRANSITION;
                    break;
            }

            _prevState = _state;
            _state = value;

        INVALID_STATE_TRANSITION:
            throw new Exception($"GameManager: invalid state transition, from {_state} to {value}");
        }
    }

    private GameID _gameID;
    public GameID GameID
    {
        get => _gameID;
        set
        {
            if (State != GameState.MainMenu)
                throw new Exception($"GameManager: attempting to modify GameID on invalid state {_state}");
            _gameID = value;
        }
    }

    // game info
    private int _level;
    public int Level
    {
        get => _level;
        set
        {
            if (_state != GameState.MainMenu && _state != GameState.OnLevel)
                throw new Exception($"GameManager: attempting to modify Level on invalid state {_state}");
            _level = value;
        }
    }

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
    OnLevel,
    OnDungeon,
}