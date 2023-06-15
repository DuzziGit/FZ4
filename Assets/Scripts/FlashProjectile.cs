using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlashProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;
    public int hasDmged;
    public int skillLevel;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        hasDmged = 0;

        skillLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().skillTwoLevel;
        damage = skillLevel * 55;
    }

    // Update is called once per frame
    void Update()
    {
                transform.Translate(Vector3.right * speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {

            if (hasDmged < 4)
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
                else

                if (hitInfo.collider.CompareTag("Cherub"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage);
                    hitInfo.collider.GetComponent<Cherub>().TakeDamage(damage);
                }
                else

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
                hasDmged++;
            }
        }


    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
        Debug.Log(skillLevel);
    }


}
