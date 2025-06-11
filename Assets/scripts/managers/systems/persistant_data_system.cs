using System;
using System.IO;
using System.Runtime.Serialization;
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
            Debug.LogError($"Error: '{ex.Message}'\nOn file mode create '{path}'");
            return false;
        }

        try
        {
            formatter.Serialize(stream, obj);
            return true;
        }
        catch (SerializationException _)
        {
            return false;
        }
        finally
        {
            stream.Close();
        }
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
            Debug.LogError($"Error: '{ex.Message}'\nOn file mode open '{path}'");
            return null;
        }

        try
        {
            T data = formatter.Deserialize(stream) as T;
            return data;
        }
        catch (SerializationException _)
        {
            return null;
        }
        finally
        {
            stream.Close();
        }
    }
}