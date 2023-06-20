using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCharText : MonoBehaviour
{ 
    public Transform target; // Reference to the Rogue character's transform
    public float yOffset = 1.5f; // Offset on the y-axis to adjust the vertical position of the text

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + new Vector3(0f, yOffset, 0f);
            transform.position = Camera.main.WorldToScreenPoint(targetPosition);
        }
    }
}
