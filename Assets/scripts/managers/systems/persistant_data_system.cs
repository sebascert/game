using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

static class PersistentDataSystem
{
    public static bool Save(string file, object obj)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, file);

        FileStream stream;
        try
        {
            stream = new FileStream(path, FileMode.Create);
        }
        catch (Exception ex)
        {
            Debug.Log($"Error: '{ex.Message}'\nOn file mode create '{path}'");
            return false;
        }

        formatter.Serialize(stream, obj);
        stream.Close();

        return true;
    }
    public static T Load<T>(string file) where T : class
    {
        string path = Path.Combine(Application.persistentDataPath, file);
        if (!File.Exists(path))
            return null;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream;
        try
        {
            stream = new FileStream(path, FileMode.Open);
        }
        catch (Exception ex)
        {
            Debug.Log($"Error: '{ex.Message}'\nOn file mode open '{path}'");
            return null;
        }

        T data = formatter.Deserialize(stream) as T;
        stream.Close();
        return data;
    }
}