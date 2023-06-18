using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float detectionRange; // Range to detect enemies
    public float maxDistance; // Maximum distance the projectile can travel
    public LayerMask enemyLayer;
    public int damage; // Base damage
    public bool hasDamaged;
    public int skillLevel;
    public float critChance; // Chance to do critical hit
    public float critMultiplier; // Critical damage multiplier

    private Vector3 initialPosition;
    private Vector3 direction;
    private Transform closestEnemy;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        initialPosition = transform.position;
        Invoke("DestroyProjectile", lifeTime);
        hasDamaged = false;
        direction = transform.right; // Set initial direction to right
    }

    private void Update()
    {
        skillLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().skillOneLevel;

        // Scaling damage with skill level
        damage = Mathf.FloorToInt(100 * Mathf.Pow(2, (skillLevel - 1) / 4)); // Base damage is 100 at skill level 1, around 1000 at skill level 5, and increases exponentially

        // Implementing critical hits
        bool isCrit = Random.value < critChance; // Randomly determine if it's a critical hit based on critChance
        if (isCrit)
            damage = Mathf.FloorToInt(damage * critMultiplier); // If it's a critical hit, multiply the damage by critMultiplier

        // Check if an enemy is within detection range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        if (colliders.Length > 0)
        {
            // Find the closest enemy
            float closestDistance = Mathf.Infinity;
            closestEnemy = null;

            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }

            // Adjust the direction to the closest enemy
            if (closestEnemy != null)
            {
                direction = (closestEnemy.position - transform.position).normalized;
            }
        }
        else
        {
            direction = transform.right; // Default direction to right when no enemy is in range
        }

        // Move the projectile
        rb.velocity = direction * speed;

        // Check if the projectile has exceeded its maximum range
        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);
        if (distanceTraveled >= maxDistance)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile collided with: " + collision.gameObject.name);
        if (!hasDamaged && collision.transform == closestEnemy)
        {
            if (collision.CompareTag("Skeleton"))
            {
                Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                collision.GetComponent<Skeleton>().TakeDamage(damage);
            }
            // Handle other enemy types if needed

            hasDamaged = true;
        }

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
     //   Destroy(gameObject);
        Debug.Log(skillLevel);
    }
}
