using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

// saving system class. Do not touch unless you know what you are doing in here!
public static class SaveState
{
    // constructor requires the player and the name of the level; the level schema will change once they are made, so for now I simply use the string of the level name.
    // We should be able to load scenes (ie levels) through their names, will double check though.
    public static void SaveGameData (GameObject player, int sceneIndex)
    {
        try
        {
            // create a new binary formatter as the save data will be in a binary format
            BinaryFormatter formatter = new BinaryFormatter();
            // unity has this cool thing called persistentPath, OS independant non-changing path that we can just use. Give the saveData a file name.
            string path = Application.persistentDataPath + "/friendZoneData.dat";

            // we create a new file stream so that we can create the file
            FileStream stream = new FileStream(path, FileMode.Create);

            // create a GameData object which houses all the data we want to save for the game.
            GameData data = new GameData(player, sceneIndex);

            // serialize and save the data to that file we defined above.
            formatter.Serialize(stream, data);

            // close the stream to avoid any file locks or other weird behaviour.
            stream.Close();
        } 
        catch (Exception e)
        {
            // something bad happend if we got here, don't worry about it.
            Debug.Log(e);
        }

    }

    // load data function
    public static GameData LoadGameData()
    {
        // use the same file path as SaveData.
        string path = Application.persistentDataPath + "/friendZoneData.dat"; 

        // CHECK if that path and file exist, if it doesn't, it means that there is no save (new game basically), or something else happened to the file.
        if (File.Exists(path))
        {
            // make anotehr binary formatter to deserialize the data we read from the save file.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            // make a new GameData object containing all the saved data, close the file and return the data object.
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            // self explanitory methinks.
            Debug.Log("Game Data was not found in " + path + ". Did something go wrong when saving the game?");
            return null;
        }
    }
}
