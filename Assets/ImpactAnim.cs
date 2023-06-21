using UnityEngine;

public class ImpactAnim : MonoBehaviour
{
    public GameObject hitEffect; // Reference to the first hit effect prefab
    public GameObject hitEffect2; // Reference to the second hit effect prefab
    public Animator animation1;
    public Animator animation2;
    public float destroyDelay = 0.1f; // Delay before destroying the projectile

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Debug.Log("hit");

            // Instantiate the hit effects at the impact point
            Vector2 collisionPoint = collision.transform.position;
            Instantiate(hitEffect, collisionPoint, Quaternion.identity);
            Instantiate(hitEffect2, collisionPoint, Quaternion.identity);

            // Play the animations for the hit effects
            if (animation1 != null)
            {
                animation1.SetBool("PlayAnimation", true);
                Debug.Log("Animation 1 Played");
            }
            if (animation2 != null)
            {
                animation2.SetBool("PlayAnimation", true);
                Debug.Log("Animation 2 Played");
            }

            // Destroy the projectile after a delay
            Destroy(collision.gameObject, destroyDelay);
        }
    }
}
