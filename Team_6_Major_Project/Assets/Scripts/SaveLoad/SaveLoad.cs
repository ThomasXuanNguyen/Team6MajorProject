﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.IO;
using UnityEngine;

public static class SaveLoad
{
    //Static function for saving data
    public static void Save(GameData saveGame)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/" + saveGame.saveGameName + ".sav", FileMode.Create);
        bf.Serialize(file, saveGame);
        file.Close();
        
    }

    //Checks if the file exist
    public static bool CheckFileExist(string gameToLoad)
    {
        if (File.Exists(Application.persistentDataPath + "/" + gameToLoad + ".sav"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Static function for loading data
    public static GameData Load(string gameToLoad)
    {
        if(File.Exists(Application.persistentDataPath + "/" + gameToLoad + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(Application.persistentDataPath + "/" + gameToLoad + ".sav", FileMode.Open);
            GameData loadedGame = (GameData)bf.Deserialize(file);
            file.Close();
            return loadedGame;
        }
        else
        {
            return null;
        }
    }

}
