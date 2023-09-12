using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Define the index of the scene to load
    public int sceneIndex = 0;

 private void OnTriggerStay2D(Collider2D collision)
{
    Debug.Log("Object entered the portal's trigger.");

    if (collision.CompareTag("Player"))
    {
        Debug.Log("Player is inside the portal's trigger.");

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Attempting to load scene.");
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

}
