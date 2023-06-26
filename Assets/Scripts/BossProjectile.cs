using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    public int projectileDamage;
    public float destroyDelay = 2f;

    private Vector3 direction;
    private bool hasDamaged = false;
    private void Start()
    {
        projectileDamage = GameObject.FindGameObjectWithTag("Cherub").GetComponent<Cherub>().damage / 2;

        StartCoroutine(DestroyAfterDelay());

        Transform target = GameObject.FindGameObjectWithTag("Player").transform; // Assign the target here
        direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotate the projectile to face the player
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Move the projectile in the initial direction

        // Removed the code for constantly tracking the player.
    }

    private void DamagePlayer(Transform player)
    {
        player.GetComponent<PlayerMovement>().UpdateHealth(-projectileDamage);
        Debug.Log("Hit Player");
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        DestroyProjectile();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !hasDamaged)
        {
            DamagePlayer(collision.transform);
            hasDamaged = true;
            DestroyProjectile();
        }
    }
}
