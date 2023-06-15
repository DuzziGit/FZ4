using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile3 : MonoBehaviour
{
    public float speed;
    public Vector2 target;
    public int projectileDamage;
    public bool hasDmged = false;



    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x + player.position.x - 2, player.position.y - 2);

        projectileDamage = GameObject.FindGameObjectWithTag("Archangel").GetComponent<Archangel>().damage / 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hasDmged)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().UpdateHealth(-projectileDamage);
                Debug.Log("Hit Player");
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
