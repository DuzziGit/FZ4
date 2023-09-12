using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int sceneIndex = 0;

    // Reference to the black square Image component
    public Image blackSquare;

    // Position to teleport the player to
    private Vector3 teleportPosition = new Vector3(-103f, 103f, 2f);

   private void OnTriggerStay2D(Collider2D collision)
{
    Debug.Log("Object entered the portal's trigger.");

    if (collision.CompareTag("Player"))
    {
        Debug.Log("Player is inside the portal's trigger.");

        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Attempting to fade out.");
            StartCoroutine(FadeAndLoadScene(collision));
        }
    }
}


  IEnumerator FadeAndLoadScene(Collider2D playerCollider)
{
    yield return StartCoroutine(FadeBlackOutSquare(true, 1));

    // Teleport the player after fade-out
    RogueSkillController rogue = playerCollider.GetComponent<RogueSkillController>();
    if (rogue != null)
    {
        rogue.transform.position = teleportPosition;
    }

    Debug.Log("Attempting to load scene.");
    SceneManager.LoadScene(sceneIndex);
}

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Debug.Log("Fade");
        Color objectColor = blackSquare.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackSquare.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackSquare.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.color = objectColor;
                yield return null;
            }
        }
    }
}
