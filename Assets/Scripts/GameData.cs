using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    // This struct contains all the game data that should be saved and loaded. Please do not touch without knowing what this is!

    // player stats
    public int playerLevel;
    public int playerHealth;
    public int playerExperience;
    public float[] position;

    // player items
    // TODO later.

    // current level
    public int sceneIndex;

    // constructor

    public GameData (GameObject player, int sceneIndex)
    {
        playerLevel = player.GetComponent<PlayerStats>().playerLevel;
        playerExperience = player.GetComponent<PlayerStats>().playerXp;
        playerHealth = player.GetComponent<PlayerStats>().playerHealth;
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        this.sceneIndex = sceneIndex;
    }

}
