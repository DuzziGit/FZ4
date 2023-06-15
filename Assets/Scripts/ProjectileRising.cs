using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRising : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;


    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                if (hitInfo.collider.CompareTag("Skeleton")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                      
                       hitInfo.collider.GetComponent<Skeleton>().TakeDamage(damage);
                    }else if (hitInfo.collider.CompareTag("Slime")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                      
                       hitInfo.collider.GetComponent<Slime>().TakeDamage(damage);
                    }else if (hitInfo.collider.CompareTag("Bat")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                      
                       hitInfo.collider.GetComponent<Bat>().TakeDamage(damage);
                    }

            }
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}