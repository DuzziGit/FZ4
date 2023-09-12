using UnityEngine;

public class dontDestroyOnload : MonoBehaviour
{
    private void Awake()
    {
        string objectTag = gameObject.tag;

        // Check if another object with the same tag exists in the scene
        GameObject[] objs = GameObject.FindGameObjectsWithTag(objectTag);

        if (objs.Length > 1)
        {
            // Destroy this object if another one with the same tag already exists
            Destroy(gameObject);
        }
        else
        {
            // Mark this object as "Don't Destroy On Load"
            DontDestroyOnLoad(gameObject);
        }
    }
}
