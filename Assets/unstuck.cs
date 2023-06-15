using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class unstuck : MonoBehaviour
{
       public Rigidbody2D rb;

   public void unstuckGame() {
        SceneManager.LoadScene("Hub World");
         GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(-2,-1,0);
                  GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 0.45f;

         
      

   }
}
