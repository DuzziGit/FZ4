using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUltimate : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;
    public int skillLevel;
    public bool hasDmged;
    public BoxCollider2D bc;

    private void Start()
    {
        hasDmged = false;
        Invoke("DestroyProjectile", lifeTime);

        skillLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ultSkillLevel; ;
        damage = 750;

        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()

    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
    
    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hasDmged)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().UpdateHealth(-damage);
                Debug.Log("Sword Hit Player");
                hasDmged = true;

            }
            DestroyProjectile();
        }
    }





    void DestroyProjectile()
    {
        Destroy(gameObject);
    }


}
