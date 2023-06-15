using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLargeStar : MonoBehaviour
{
        public float speed;
        public float lifeTime;
        public float distance;
        public LayerMask whatIsSolid;
        public int damage;
         public int skillLevel;
    public bool hasDmged;
    public BoxCollider2D bc;

    private void Start(){
        Invoke("DestroyProjectile", lifeTime);
              skillLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().skillTwoLevel;
        damage = skillLevel * 64;

                bc = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
                            transform.Translate(Vector3.right * speed * Time.deltaTime);
                            
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance,whatIsSolid);

                if (hitInfo.collider != null){
                    if (!hasDmged)
                {
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
                       if (hitInfo.collider.CompareTag("Skeleton")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                      hasDmged = true;
                      
                       hitInfo.collider.GetComponent<Skeleton>().TakeDamage(damage);
                    }else if (hitInfo.collider.CompareTag("Slime")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                      hasDmged = true;
                       hitInfo.collider.GetComponent<Slime>().TakeDamage(damage);
                    }else if (hitInfo.collider.CompareTag("Bat")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage); 
                      hasDmged = true;
                    hitInfo.collider.GetComponent<Bat>().TakeDamage(damage);
                    }

                }
                    }
     void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Slime" ||  collision.gameObject.tag == "Skeleton" )
        {
           hasDmged = true;
        }
                

                }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
     if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Slime" ||  collision.gameObject.tag == "Skeleton" )
        {
           hasDmged = false;
        }
            }


    
    
    void DestroyProjectile(){
        Destroy(gameObject);
    }


}

