using System;
using System.IO;
using System.Linq;

using UnityEngine;

class GameSaveSystem : GameSystem<GameSaveSystem>
{
    public const string SaveFileName = "game-";
    public const string SaveDir = "saves";

    public const int SlotCount = 3;
    public GameID[] Slots { get; private set; }

    public override void Init()
    {
        if (!Directory.Exists(SaveDir))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, SaveDir));

        Slots = new GameID[SlotCount];
        string[] saves = Directory.GetFiles(Path.Combine(Application.persistentDataPath, SaveDir), $"{SaveDir}game-*");
        if (saves.Length > SlotCount)
            throw new Exception("GameSaveSystem: more saved games than slots");

        foreach (string save in saves)
        {
            GameID gameID = new GameID(save);
            Slots[gameID.value] = gameID;
        }
    }

    /// return save if saved in given slot (GameID), throws UnableToLoadGameException
    public GameSave Load(GameID id)
    {
        if (id.value > SlotCount)
            throw new Exception("GameSaveSystem: invalid GameID provided");

        if (Slots[id.value] == null)
            return null;

        GameSave save = PersistentDataSystem.Load<GameSave>(id.Path());

        if (save == null)
            throw new UnableToLoadGameException();
        return save;
    }

    /// return whether the game was saved, throws UnableToSaveGameException
    public bool NewSave(GameSave save)
    {
        GameID id = Slots.FirstOrDefault(slot => slot != null);
        if (id == null)
            return false;

        if (!PersistentDataSystem.Save(id.Path(), save))
            throw new UnableToSaveGameException();
        return true;
    }

    /// return true if saved in slot, false if there's a saved game in given slot
    /// throws UnableToLoadGameException
    public bool SaveInSlot(GameSave save, GameID id)
    {
        if (id.value > SlotCount)
            throw new Exception("GameSaveSystem: invalid GameID provided");

        if (Slots[id.value] != null)
            return false;

        if (!PersistentDataSystem.Save(id.Path(), save))
            throw new UnableToSaveGameException();
        return true;
    }

    /// return whether given slot had a saved game, if so, it overwrites it
    /// throws UnableToLoadGameException
    public bool OverwriteSlot(GameSave save, GameID id)
    {
        if (id.value > SlotCount)
            throw new Exception("GameSaveSystem: invalid GameID provided");

        if (Slots[id.value] == null)
            return false;

        if (!PersistentDataSystem.Save(id.Path(), save))
            throw new UnableToSaveGameException();
        return true;
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

    public GameSave()
    {
        level = 0;
        playerLife = 0; //INITIAL_PLAYER_LIFE;
    }
}

public class UnableToSaveGameException : Exception
{
}
public class UnableToLoadGameException : Exception
{
}