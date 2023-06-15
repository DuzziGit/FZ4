using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
   

    public void MainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
