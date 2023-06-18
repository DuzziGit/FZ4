using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumController : MonoBehaviour
{
    public float moveSpeed = 0.05f;
    public float fadeSpeed = 20f;

    private TMP_Text tmpComponent;
    private Color originalColor;

    private void Start()
    {
        tmpComponent = GetComponent<TMP_Text>();
        if (tmpComponent != null)
        {
            originalColor = tmpComponent.color;
        }
    }
    private void Update()
    {
        // Move upward
        transform.position += new Vector3(0, moveSpeed, 0);

        if (tmpComponent != null)
        {
            // Fade out
            Color currentColor = tmpComponent.color;
            float alphaDecrease = fadeSpeed * Time.deltaTime;
            currentColor.a -= alphaDecrease;
            tmpComponent.color = currentColor;

            // Destroy the GameObject when it is fully transparent
            if (currentColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Reset the color when setting new damage number
    public void SetDamageNum(int damage)
    {
        tmpComponent = GetComponent<TMP_Text>();
        if (tmpComponent != null)
        {
            originalColor = tmpComponent.color;
            tmpComponent.text = damage.ToString();
            tmpComponent.color = originalColor;
        }
    }
}