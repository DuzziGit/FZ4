using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;
    public bool hasDmged;
    public int skillLevel;
    public float critChance; // Chance to do critical hit
    public float critMultiplier; // Critical damage multiplier

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        hasDmged = false;
    }

    // Update is called once per frame
    void Update()
    {
        skillLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().skillOneLevel;

        // Scaling damage with skill level
        damage = Mathf.FloorToInt(100 * Mathf.Pow(2, (skillLevel - 1) / 4)); // Base damage is 100 at skill level 1, around 1000 at skill level 5, and increases exponentially

        // Implementing critical hits
        bool isCrit = Random.value < critChance; // Randomly determine if it's a critical hit based on critChance
        if (isCrit)
            damage = Mathf.FloorToInt(damage * critMultiplier); // If it's a critical hit, multiply the damage by critMultiplier

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (!hasDmged)
            {

                if (hitInfo.collider.CompareTag("Seraphim"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Seraphim>().TakeDamage(damage);
                }
                else

                if (hitInfo.collider.CompareTag("Archangel"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Archangel>().TakeDamage(damage);
                }


                else if(hitInfo.collider.CompareTag("Cherub"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Cherub>().TakeDamage(damage);
                }

                if (hitInfo.collider.CompareTag("Bat"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Bat>().TakeDamage(damage);
                }
                else if (hitInfo.collider.CompareTag("Slime"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Slime>().TakeDamage(damage);

                }
                else if (hitInfo.collider.CompareTag("Skeleton"))
                {

                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Skeleton>().TakeDamage(damage);
                }
                else if (hitInfo.collider.CompareTag("Enemy"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                }
                hasDmged = true;
            }
            DestroyProjectile();
        }

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
        Debug.Log(skillLevel);
    }
}
