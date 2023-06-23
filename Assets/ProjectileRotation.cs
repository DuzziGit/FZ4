using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    public float rotationSpeed = 180f; // Adjust the speed of rotation as per your preference
    private bool isProjectileActive = true;

    private void Update()
    {
        if (isProjectileActive)
        {
            // Rotate the projectile around its z-axis based on the rotationSpeed
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    // Call this method when you want to deactivate the projectile
    public void DeactivateProjectile()
    {
        isProjectileActive = false;
    }
}
