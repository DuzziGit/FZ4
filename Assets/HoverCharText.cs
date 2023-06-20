using UnityEngine;

public class HoverCharText : MonoBehaviour
{
    public Transform target; // Reference to the Rogue character's transform
    public float yOffset = 1.5f; // Offset on the y-axis to adjust the vertical position of the text
    public float smoothing = 5f; // Smoothing factor for movement

    private Camera mainCamera;
    private RectTransform canvasRectTransform;
    private RectTransform textRectTransform;
    private Vector3 targetPosition;
    private Vector3 smoothVelocity;

    private void Awake()
    {
        mainCamera = Camera.main;
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        textRectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            targetPosition = target.position + new Vector3(0f, yOffset, 0f);
        }

        Vector3 worldToViewportPoint = mainCamera.WorldToViewportPoint(targetPosition);
        Vector2 anchoredPosition = new Vector2(
            (worldToViewportPoint.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f),
            (worldToViewportPoint.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)
        );

        textRectTransform.anchoredPosition = Vector3.SmoothDamp(textRectTransform.anchoredPosition, anchoredPosition, ref smoothVelocity, smoothing);
    }
}
