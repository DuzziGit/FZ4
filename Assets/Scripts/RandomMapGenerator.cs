using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numPlatformsX = 16;
    public int numPlatformsY = 9;
    public float platformWidth = 1f;
    public float platformHeight = 1f;

    void Start()
    {
        for (int y = 0; y < numPlatformsY; y++)
        {
            for (int x = 0; x < numPlatformsX; x++)
            {
                Vector3 platformPos = new Vector3(
                    (x - (numPlatformsX - 1) / 2f) * platformWidth,
                    y * platformHeight,
                    0f
                );

                GameObject platform = Instantiate(platformPrefab, platformPos, Quaternion.identity);
                platform.transform.parent = transform;
            }
        }
    }
}