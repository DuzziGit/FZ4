using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float displayTime = 1.5f;

    private TextMeshPro textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshPro>();
        Debug.Log("Text Component: " + textComponent);
    }

    private void Start()
    {
        StartCoroutine(ShowAndDestroy());
    }

    IEnumerator ShowAndDestroy()
    {
        yield return new WaitForSeconds(displayTime);
        Destroy(gameObject);
    }
}
