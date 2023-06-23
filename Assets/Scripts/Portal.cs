using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Define the index of the scene to load
    public int sceneIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Display a message or perform any necessary checks before teleporting

            // Check if the "Up Arrow" or "I" key is pressed
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.I))
            {
                // Load the scene at the specified index
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
