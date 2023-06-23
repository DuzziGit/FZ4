using UnityEngine;

public class DontDestroyCamera : MonoBehaviour
{
    private void Awake()
    {
        // Check if another camera object exists in the scene
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (cameras.Length > 1)
        {
            // Destroy this camera if another one already exists
            Destroy(gameObject);
        }
        else
        {
            // Mark this camera as "Don't Destroy On Load"
            DontDestroyOnLoad(gameObject);
        }
    }
}
