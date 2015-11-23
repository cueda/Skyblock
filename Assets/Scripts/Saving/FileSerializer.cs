using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class FileSerializer 
{
    public static FileData fileData;

    
    public static void Save()
    {
        // Create save data
        fileData = new FileData();
        fileData.PopulateData();

        // Write save data to data file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveGame.data");
        bf.Serialize(file, fileData);
        file.Close();
    }


    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveGame.data"))
        {
            // Read save data from data file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveGame.data", FileMode.Open);
            fileData = (FileData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("Save data not found.");
        }
    }


    public static void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/saveGame.data"))
        {
            // Delete data file
            File.Delete(Application.persistentDataPath + "/saveGame.data");
        }

        // Load game with default settings
        Application.LoadLevel(Application.loadedLevel);
    }
}
