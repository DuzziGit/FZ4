using System.Collections.Generic;
using UnityEngine;

public class scrio : MonoBehaviour
{
    public int maxPlatforms = 10;
    public GameObject[] platformPrefabs;
    public float xPadding = 1f;
    public float yPadding = 1f;
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    private List<GameObject> platforms = new List<GameObject>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxSpawnPosition.x - minSpawnPosition.x, maxSpawnPosition.y - minSpawnPosition.y, 0));
    }

    private void Start()
    {
        int numPlatforms = Random.Range(8, 12);

        for (int i = 0; i < numPlatforms && platforms.Count < maxPlatforms; i++)
        {
            // Choose a random platform prefab
            GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

            // Get the size of the prefab and adjust for padding
            Vector2 size = prefab.GetComponent<BoxCollider2D>().bounds.size;
            size += new Vector2(xPadding * 2, yPadding * 2);

            // Choose a random position for the platform
            Vector2 position = new Vector2(Random.Range(minSpawnPosition.x, maxSpawnPosition.x - size.x), Random.Range(minSpawnPosition.y, maxSpawnPosition.y - size.y));

            // Check if the platform overlaps with any existing platforms
            bool overlap = false;
            foreach (GameObject platform in platforms)
            {
                if (Mathf.Abs(platform.transform.position.x - position.x) < (size.x + platform.GetComponent<BoxCollider2D>().bounds.size.x) / 2 &&
                    Mathf.Abs(platform.transform.position.y - position.y) < (size.y + platform.GetComponent<BoxCollider2D>().bounds.size.y) / 2)
                {
                    overlap = true;
                    break;
                }
            }

            // If the platform doesn't overlap, create it and add it to the list
            if (!overlap)
            {
                GameObject platform = Instantiate(prefab, position, Quaternion.identity);
                platforms.Add(platform);
            }
        }
    }
}