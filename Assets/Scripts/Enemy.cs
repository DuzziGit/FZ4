using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int level;
    public float speed;
    public float distance;

    public Transform groundDetection;
    public Transform wallDetection;


    public int expValue;
    public ExperienceController expObject;
     public int coinValue;
    public coinController coinObject;
    public static bool isAggroed = false;
    public static bool isPatroling = true;
    public static bool isTouchingPlayer = false;
    private bool movingRight = true;

    public float agroRange;

    private float timeBetweenDmg;
    public float startTimeBetweenDmg;
    public SpriteRenderer enemySprite;

    float moveSpeed;
    public BoxCollider2D bc;
    public Rigidbody2D rb;

    public Color bigEnemy = Color.red;
    public Color medEnemy = Color.yellow;
    public Color smallEnemy = Color.green;
    public Color tutEnemy = new Color(0, 1f, 1f, 1f);




    public Animator animator;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        timeBetweenDmg = startTimeBetweenDmg;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Debug.Log("Enemy Touching Player");
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          //  Debug.Log("Enemy Not Touching Player");
            isTouchingPlayer = false;
        }
    }

    void Update()
    {
        if (isTouchingPlayer)
        {
            if (timeBetweenDmg <= 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().UpdateHealth(-damage);
                timeBetweenDmg = startTimeBetweenDmg;
            }
            else
            {
                timeBetweenDmg -= Time.deltaTime;
            }
        }

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentHealth <= 0)
		{
            isTouchingPlayer = false;
		}
    }


    public void HoverPlayerX()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, LayerMask.GetMask("World"));
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, distance, LayerMask.GetMask("World"));
        RaycastHit2D enemyWall = Physics2D.Raycast(wallDetection.position, Vector2.right, distance, LayerMask.GetMask("EnemyOnlyWall"));

        if (transform.position.x - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x < 0.2f)
		{
            rb.velocity = new Vector3(0, 0, 0);
         //   Debug.Log("on top");
        } else
        /*
        if (isAggroed && enemyWall.collider == true)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            Debug.Log("agroed but touching wall");

        } else
        */

        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x && enemyWall.collider == false)
            {

                //Move Left
                // rb.velocity = new Vector2(-moveSpeed, 0);
               //   rb.velocity = new Vector3(-moveSpeed, rb.velocity.y);
               // Debug.Log("going left");
                rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
                enemySprite.flipX = true;
                movingRight = false;

        }

            if (transform.position.x < GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x && enemyWall.collider == false)
            {


            //Move Right
            //rb.velocity = new Vector2(moveSpeed, 0);
            // rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
        //    Debug.Log("going right");
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
                enemySprite.flipX = false;
                movingRight = true;
        }

    }




    private void FixedUpdate() { 

        if (health <= 0) {

                  EnemySpawner.currentEnemies -= 1;
                 // Debug.Log("Current Enemies" + EnemySpawner.currentEnemies);
            ExperienceController exp = Instantiate(expObject, transform.position, transform.rotation);
            ExperienceController.experience = expValue;
            coinController coin = Instantiate(coinObject, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z) , transform.rotation);
            coinController.coin = coinValue;

            Destroy(gameObject);
            }

        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);

		if (distanceToPlayer < agroRange)
		{
            isAggroed = true;
            isPatroling = false;
            if (isAggroed)
            {
                chasePlayer();
            }
        } else
		{ 
            if (distanceToPlayer > agroRange)
            {
                isAggroed = false;




                isPatroling = true;
                if (isAggroed)
                {
                    chasePlayer();
                }
            }
                Patrol();
                isAggroed = false;
                isPatroling = true;       
        }
 }

    private void Patrol()
	{
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, LayerMask.GetMask("World"));
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, distance, LayerMask.GetMask("World"));
        RaycastHit2D enemyWall = Physics2D.Raycast(wallDetection.position, Vector2.right, distance, LayerMask.GetMask("EnemyOnlyWall"));


        if (groundInfo.collider == false || wallInfo.collider == true || enemyWall.collider == true)
        {
            if (movingRight == true)
            {
                rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
                enemySprite.flipX = true;
                movingRight = false;
            }
            else
            {
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
                enemySprite.flipX = false;
                movingRight = true;
            }
        }
    }




    void chasePlayer()
	{
        HoverPlayerX();
	}





}
