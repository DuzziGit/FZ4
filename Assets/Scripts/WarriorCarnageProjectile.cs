using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCarnageProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    // layermask to determine what layers the raycast collider should be focused on
    public LayerMask whatIsSolid;
    public int damage;
    public int degrees;
    public int skillLevel;
    public BoxCollider2D bc;
    public bool hasDmged;


    private void Start()
    {
                Invoke("DestroyProjectile", lifeTime);


        skillLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ultSkillLevel;
        damage = skillLevel * 250;
                bc = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // create a raycast going down from the sword
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(0, -0.1f), 0.1f, whatIsSolid);
        // draw the ray for debugging
        Debug.DrawRay(transform.position, new Vector2(0, -0.1f), Color.red, 0.01f);

        // check to see if the raycast his *something*
        if (hitInfo.collider != null)
        {
                                if (!hasDmged)
               if (hitInfo.collider.CompareTag("Seraphim"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Seraphim>().TakeDamage(damage);
                                                                hasDmged = true;

                }
                else

                if (hitInfo.collider.CompareTag("Archangel"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Archangel>().TakeDamage(damage);
                                                                hasDmged = true;

                }
                else

                if (hitInfo.collider.CompareTag("Cherub"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Cherub>().TakeDamage(damage);
                                                                hasDmged = true;

                }
                else
            // if the raycast hit an enemy, get the enemy that it and apply damage, delete the sword after
              if (hitInfo.collider.CompareTag("Skeleton")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                                            hasDmged = true;

                       hitInfo.collider.GetComponent<Skeleton>().TakeDamage(damage);
                               Destroy(this.gameObject);

                    }else if (hitInfo.collider.CompareTag("Slime")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                                            hasDmged = true;

                       hitInfo.collider.GetComponent<Slime>().TakeDamage(damage);
                               Destroy(this.gameObject);

                    }else if (hitInfo.collider.CompareTag("Bat")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                                            hasDmged = true;

                       hitInfo.collider.GetComponent<Bat>().TakeDamage(damage);
                               Destroy(this.gameObject);

                    }

            
            // if the sword hit the world, just delete it
            if (hitInfo.collider.CompareTag("World") || hitInfo.collider.CompareTag("Platform"))
            {
        Destroy(this.gameObject);
            }
        }

        //transform.rotation = Quaternion.Euler(Vector3.forward * degrees);

        //transform.Translate(Vector3.right * speed * Time.deltaTime);
    }


        void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Slime" ||  collision.gameObject.tag == "Skeleton" )
        {
           hasDmged = true;
        }
                

                }


    

    void OnTriggerExit2D(Collider2D collision)
    {
     if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Slime" ||  collision.gameObject.tag == "Skeleton" )
        {
           hasDmged = false;
        }
            }


    // delete the sword projectile function
      void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

}
