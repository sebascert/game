using System;
using System.IO;
using System.Linq;

using UnityEngine;

class GameSaveSystem : GameSystem
{
    public const string saveFileName = "game-";
    public const string saveDir = "saves";

    public const int slotCount = 3;
    private GameID[] slots;

    private void Start()
    {
        slots = new GameID[slotCount];
        string[] saves = Directory.GetFiles(Path.Combine(Application.persistentDataPath, saveDir), $"{saveDir}game-*");
        if (saves.Length > slotCount)
            throw new Exception("GameSaveSystem: more saved games than slots");

        foreach (string save in saves)
        {
            GameID gameID = new GameID(save);
            slots[gameID.value] = gameID;
        }
    }

    public GameSave Load(GameID id)
    {
        if (slots[id.value] == null)
            return null;

        return PersistentDataSystem.Load<GameSave>(id.Path());
    }

    public bool NewSave(GameSave save)
    {
        GameID id = slots.FirstOrDefault(slot => slot != null);
        if (id == null)
            return false;

        return PersistentDataSystem.Save(id.Path(), save);
    }

    public bool SaveInSlot(GameSave save, GameID id)
    {
        if (id.value > slotCount)
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
        return System.IO.Path.Combine(GameSaveSystem.saveDir, GameSaveSystem.saveFileName, value.ToString());
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