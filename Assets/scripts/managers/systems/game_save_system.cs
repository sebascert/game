using System;
using System.IO;
using System.Linq;

using UnityEngine;

class GameSaveSystem : GameSystem
{
    public const string SaveFileName = "game-";
    public const string SaveDir = "saves";

    public const int SlotCount = 3;
    private GameID[] _slots;

    private void Start()
    {
        _slots = new GameID[SlotCount];
        string[] saves = Directory.GetFiles(Path.Combine(Application.persistentDataPath, SaveDir), $"{SaveDir}game-*");
        if (saves.Length > SlotCount)
            throw new Exception("GameSaveSystem: more saved games than slots");

        foreach (string save in saves)
        {
            GameID gameID = new GameID(save);
            _slots[gameID.value] = gameID;
        }
    }

    public GameSave Load(GameID id)
    {
        if (_slots[id.value] == null)
            return null;

        return PersistentDataSystem.Load<GameSave>(id.Path());
    }

    public bool NewSave(GameSave save)
    {
        GameID id = _slots.FirstOrDefault(slot => slot != null);
        if (id == null)
            return false;

        return PersistentDataSystem.Save(id.Path(), save);
    }

    public bool SaveInSlot(GameSave save, GameID id)
    {
        if (id.value > SlotCount)
        {
            Debug.LogWarning("GameSaveSystem: invalid GameID provided to SaveInSlot");
            return false;
        }

        return PersistentDataSystem.Save(id.Path(), save);
    }

}

public class GameID
{
    public int value;

    public GameID(int value)
    {
        this.value = value;
    }

    public GameID(string path)
    {
        this.value = int.Parse(path.Split('-')[1]);
    }
    public string Path()
    {
        return System.IO.Path.Combine(GameSaveSystem.SaveDir, GameSaveSystem.SaveFileName, value.ToString());
    }
}
[Serializable]
class GameSave
{
    // dungeons
    public int level;

    // player
    public int playerLife;
}