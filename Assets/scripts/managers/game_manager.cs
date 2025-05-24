using System;
using System.Collections;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    private GameSystemBase[] gameSystems;

    [SerializeField]
    private SceneAsset[] levelScenes;
    private SceneAsset _loadedLevel;

    private Coroutine _runningCoroutine;

    private void Start()
    {
        //custom Start for game systems, as their singleton property is asserted in Awake
        foreach (GameSystemBase gameSystem in gameSystems)
            gameSystem.Init();
    }

    public void StartGame(GameID id)
    {
        if (_runningCoroutine != null)
            return;
        _runningCoroutine = StartCoroutine(StartGameCoroutine(id));
    }
    private IEnumerator StartGameCoroutine(GameID id)
    {
        GameSave savedGame = GameSaveSystem.Instance.Load(id);
        if (savedGame == null)
        {
            savedGame = new GameSave();
            GameSaveSystem.Instance.NewSave(savedGame);
        }

        GameStateSystem.Instance.GameID = id;
        GameStateSystem.Instance.State = GameState.OnLevel;
        GameStateSystem.Instance.Level = savedGame.level;

        yield return StartCoroutine(CinematicSystem.Instance.PlayCinematic($"enter-level-{savedGame.level}"));
        yield return StartCoroutine(LoadLevel(savedGame.level));

        _runningCoroutine = null;
    }

    public void NextLevel()
    {
        if (_runningCoroutine != null)
            return;
        _runningCoroutine = StartCoroutine(NextLevelCoroutine());
    }
    private IEnumerator NextLevelCoroutine()
    {
        GameStateSystem.Instance.Level += 1;

        // finished game
        if (GameStateSystem.Instance.Level == levelScenes.Length)
        {
            GameSaveSystem.Instance.Remove(GameStateSystem.Instance.GameID);

            GameStateSystem.Instance.State = GameState.MainMenu;
            GameStateSystem.Instance.GameID = null;

            yield return StartCoroutine(CinematicSystem.Instance.PlayCinematic("endgame"));
            yield return StartCoroutine(UnloadLevel());

            yield break;
        }

        int level = GameStateSystem.Instance.Level;
        yield return StartCoroutine(CinematicSystem.Instance.PlayCinematic($"enter-level-{level}"));
        yield return StartCoroutine(UnloadLevel());
        yield return StartCoroutine(LoadLevel(GameStateSystem.Instance.Level));

        _runningCoroutine = null;
    }

    public void LoseGame()
    {
        if (_runningCoroutine != null)
            return;
        _runningCoroutine = StartCoroutine(LoseGameCoroutine());
    }
    private IEnumerator LoseGameCoroutine()
    {
        GameSaveSystem.Instance.Remove(GameStateSystem.Instance.GameID);

        GameStateSystem.Instance.State = GameState.MainMenu;
        GameStateSystem.Instance.GameID = null;

        yield return StartCoroutine(CinematicSystem.Instance.PlayCinematic("losegame"));
        yield return StartCoroutine(UnloadLevel());

        _runningCoroutine = null;
    }
    public void QuitGame()
    {
        if (_runningCoroutine != null)
            return;
        _runningCoroutine = StartCoroutine(QuitGameCoroutine());
    }
    private IEnumerator QuitGameCoroutine()
    {
        SaveGame();

        GameStateSystem.Instance.State = GameState.MainMenu;
        GameStateSystem.Instance.GameID = null;

        yield return StartCoroutine(CinematicSystem.Instance.PlayCinematic("quitgame"));
        yield return StartCoroutine(UnloadLevel());

        _runningCoroutine = null;
    }

    private IEnumerator LoadLevel(int level)
    {
        SceneAsset levelScene = levelScenes[level];
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelScene.name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncLoad is { isDone: true });
        _loadedLevel = levelScene;
    }
    private IEnumerator UnloadLevel()
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(_loadedLevel.name);
        yield return new WaitUntil(() => asyncUnload is { isDone: true });
        _loadedLevel = null;
    }

    private void SaveGame()
    {
        GameSave save = new GameSave();
        save.level = GameStateSystem.Instance.Level;
        save.playerLife = 0; //GameStateSystem.Instance.playerLife;

        GameSaveSystem.Instance.SaveInSlot(save, GameStateSystem.Instance.GameID);
    }

    private void OnDestroy()
    {
        if (GameStateSystem.Instance.GameID == null)
            return;
        SaveGame();
    }
}