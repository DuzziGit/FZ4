using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;


public class SceneLoader : MonoBehaviour
{

    public AudioMixer masterMixer;


    public void PlayGame() {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void CreditsScreen()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ScreenChange()
    {
        // Toggles fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }

  

}
