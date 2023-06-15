using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{

public GameObject Rogue;
public GameObject Warrior;
public GameObject Mage;


    public void RogueSelect() {
            SceneManager.LoadScene("World 0-1");
                        Instantiate(Rogue,new Vector3(-6, 5, 0), Quaternion.identity);
                 GameObject.FindGameObjectWithTag("Music").GetComponent<musicClass>().StopMusic();

    }

       public void MageSelect() {
            SceneManager.LoadScene("World 0-1");
            Instantiate(Mage,new Vector3(-6, 5, 0), Quaternion.identity);
                 GameObject.FindGameObjectWithTag("Music").GetComponent<musicClass>().StopMusic();

    }
   public void WarriorSelect() {
            SceneManager.LoadScene("World 0-1");
            Instantiate(Warrior,new Vector3(-6, 5, 0), Quaternion.identity);
                             GameObject.FindGameObjectWithTag("Music").GetComponent<musicClass>().StopMusic();

    }


    // public void characterSelect()
    // {
    //     if 
    // }

}
