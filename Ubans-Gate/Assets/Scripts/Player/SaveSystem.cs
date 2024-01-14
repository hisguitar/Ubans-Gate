using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (Player player)
    {
        // It creates a BinaryFormatter to convert data into a binary file.
        BinaryFormatter formatter = new();

        // Specifies the path to store the data using Application.persistentDataPath to make it accessible on Windows, Android, iOS.
        string path = Application.persistentDataPath + "/player.data";

        // Opens a FileStream in FileMode.Create to create a new file or overwrite an existing one.
        FileStream stream = new(path, FileMode.Create);

        // Save 'data' from 'player'
        PlayerData data = new(player);

        // Uses BinaryFormatter to serialize 'data' into binary and saves it to the FileStream.
        formatter.Serialize(stream, data);

        // Closes the FileStream to release resources.
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        // Specifies the path to the data file (same as the SavePlayer function).
        string path = Application.persistentDataPath + "/player.data";

        // Checks if the data file exists; if not, it logs an error.
        if (File.Exists(path))
        {
            // Opens a FileStream in FileMode.Open to read the data file.
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            // Uses BinaryFormatter to deserialize the binary 'data' into a 'PlayerData' object and returns it to the game.
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            // Closes the FileStream to release resources.
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}