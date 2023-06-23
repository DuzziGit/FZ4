using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPortalInteraction : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.I))
        {
            // Check if the player is colliding with a portal
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Adjust the radius as needed

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Portal"))
                {
                    // Load the scene at the specified index
                    SceneManager.LoadScene(collider.GetComponent<Portal>().sceneIndex);
                    break;
                }
            }
        }
    }
}
