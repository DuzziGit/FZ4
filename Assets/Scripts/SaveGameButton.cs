using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameButton : MonoBehaviour
{
    public void SaveGame()
    {
        // save the game when the button is pressed
        SaveState.SaveGameData(GameObject.FindGameObjectWithTag("Player"), SceneManager.GetActiveScene().buildIndex);
    }
}
