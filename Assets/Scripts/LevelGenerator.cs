using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class LevelGenerator : MonoBehaviour
{
    public GameObject[] platforms; // Array of platform prefabs
    public GameObject background; // Reference to the background object
    public float padding = 1f; // Padding between platforms
    public int maxTries = 100; // Maximum number of tries to find non-overlapping positions
    public int maxPlatforms = 8; // Maximum number of platforms to spawn
    public float minDistance = 5f; // Minimum distance between platforms

    void Start()
    {
        float maxX = background.transform.position.x + (background.transform.localScale.x /0.3f); // Maximum x position for platforms
        float maxY = background.transform.position.y + (background.transform.localScale.y /0.1f); // Maximum y position for platforms
        List<GameObject> placedPlatforms = new List<GameObject>(); // List of placed platforms

        // Loop until the maximum number of platforms is reached or no more non-overlapping positions can be found
        for (int i = 0; i < maxPlatforms && i < platforms.Length; i++)
        {
            int tries = 0;
            bool overlaps = true;
            GameObject platformPrefab = platforms[i];
            GameObject platform = Instantiate(platformPrefab, transform); // Instantiate a new platform

            // Loop until a non-overlapping position is found or the maximum number of tries is reached
            while (overlaps && tries < maxTries)
            {
                // Generate random x and y positions for the platform
                float randomX = Random.Range(-maxX, maxX);
                float randomY = Random.Range(-maxY, maxY);

                // Set the platform's position
                platform.transform.position = new Vector2(randomX, randomY);

                // Check if the platform overlaps with any previously placed platforms
                overlaps = false;
                foreach (GameObject placedPlatform in placedPlatforms)
                {
                    float distance = Vector2.Distance(platform.transform.position, placedPlatform.transform.position);
                    if (distance < minDistance)
                    {
                        overlaps = true;
                        break;
                    }
                }

                tries++;
            }

            // Check if a non-overlapping position was found
            if (!overlaps)
            {
                placedPlatforms.Add(platform);
            }
            else
            {
                Destroy(platform);
            }
        }
    }
}