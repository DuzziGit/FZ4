using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherub : MonoBehaviour
{
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject bossProjectile;

    public int health;
    public int damage;

    public int expValue;
    public ExperienceController expObject;
    public static bool isAggroed = false;

    public float speed;
    public float distance;

    private bool movingRight = true;
    public Transform groundDetection;

    public static bool facingRight = true;
    public static bool patrol = false;
    public float moveDirection;
    private SpriteRenderer bossSprite;

    public Vector3 startPosition;

    public float distanceFromStart;

    public bool startup = true;
    public bool phase2Started = true;
    public bool phase2 = false;

    public GameObject portal;

    public bool isTouchingPlayer = false;

    private float timeBetweenDmg;
    public float startTimeBetweenDmg;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public Animator animator;

    public TextMesh damageDisplay;

    public HealthBar healthBar;
    public int maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        HealthBar healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }


        rb.velocity = new Vector3(-speed, 1, 0);
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;

    }

    void Awake()
    {
        bossSprite = GetComponent<SpriteRenderer>();
        startup = true;
        bossSprite.flipX = true;
    }

    // Update is called once per frame
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

        var LootGenerator = (Random.Range(1, 100));


        if (health <= 0)
        {
            StartCoroutine(animationVanish());
        }

        healthBar.SetHealth(health);


        getStartPosition();

        if (health >= 24000)
        {
            Bounce();
            
        } else if (health < 15000 && health > 1000)
        {
            animator.SetBool("isShooting", true);
            Agressive();
        }
        else if (health < 1000)
        {
            damage *= 10;
            animator.SetBool("isShooting", false);
            phase2 = true;
            rotateAroundMap();
        }
       
    }

    public void TakeDamage(int damage)
    {
        animator.SetBool("Damage", true);
        StartCoroutine(animationDelay());
        StartCoroutine(DamageDisplay(damage));
        health -= damage;
        Debug.Log("Health: " + health);
       
    }

    void Agressive() {

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);


        
        if (timeBetweenShots <= 0)
        {
            Instantiate(bossProjectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }


        if (distanceToPlayer < agroRange)
        {
            chasePlayer();
        }

    }


    private void chasePlayer()
    {
        StartCoroutine(HoverPlayerX());
    }


    void rotateAroundMap()
	{
        if (phase2Started)
        {
            if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x)
            {
             
                rb.velocity = new Vector3(-speed - 2, 0, 0);

            }

            if (transform.position.x < GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x)
            {
                rb.velocity = new Vector3(speed + 2, 0, 0);

            }
            
            Debug.Log(startPosition);
            phase2Started = false;
        }
    }

    void getStartPosition()
    {
        if (startup)
        {
            startPosition = transform.position;
            Debug.Log(startPosition);
            startup = false;
        }
    }


    void Patrol()
    {

        //Return To Start Position
        if (transform.position.x > startPosition.x)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            bossSprite.flipX = true;
            Debug.Log("going left");
        } else if (transform.position.x < startPosition.x)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            bossSprite.flipX = false;
            Debug.Log("going right");
        }



    }


    void Bounce()
    {
        OnTriggerEnter2D(bc);

    }

    void HoverPlayerY()
    {
        
        if (transform.position.y > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.y + 1)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        

        if (transform.position.y < GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.y + 1)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }
    IEnumerator HoverPlayerX()
    {
        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x)
        {
            yield return new WaitForSeconds(0.5f);
            //Move Left
            // rb.velocity = new Vector2(-moveSpeed, 0);
            //   rb.velocity = new Vector3(-moveSpeed, rb.velocity.y);
            Debug.Log("going left");
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
            bossSprite.flipX = true;

        }

        if (transform.position.x < GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x)
        {
            yield return new WaitForSeconds(0.5f);
            //Move Right
            //rb.velocity = new Vector2(moveSpeed, 0);
            // rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
            Debug.Log("going right");
            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
            bossSprite.flipX = false;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy Touching Player");
            isTouchingPlayer = true;
        }

        if (!phase2)
        {

            if (bossSprite.flipX == true)
            {
                if (collision.gameObject.tag == "Wall")
                {
                    Debug.Log("left wall");
                    rb.velocity = new Vector3(speed + 1, rb.velocity.y, 0);
                    bossSprite.flipX = false;
                }
            }
            else if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("right wall");
                rb.velocity = new Vector3(-speed - 1, rb.velocity.y, 0);
                bossSprite.flipX = true;
            }

            if (collision.gameObject.tag == "Roof")
            {
                Debug.Log("Roof");
                rb.velocity = new Vector3(rb.velocity.x, -1, 0);
            }
            if (collision.gameObject.tag == "World")
            {
                Debug.Log("Roof");
                rb.velocity = new Vector3(rb.velocity.x, 1, 0);
            }
        } else
		{
            if(bossSprite.flipX == true)
            {
                if (collision.gameObject.tag == "Wall")
                {
                    Debug.Log("left wall");
                    rb.velocity = new Vector3(0, -1, 0);
                    bossSprite.flipX = false;
                }
            }
            else if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("right wall");
                rb.velocity = new Vector3(0, 1, 0);
                bossSprite.flipX = true;
            }

            if (collision.gameObject.tag == "Roof")
            {
                
                Debug.Log("Roof");
                StartCoroutine(floatDown());
            }
            if (collision.gameObject.tag == "World")
            {
                Debug.Log("Floor");
                rb.velocity = new Vector3(speed + 2, 0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy Not Touching Player");
            isTouchingPlayer = false;
        }
    }

    IEnumerator animationDelay()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("Damage", false);
    }

    IEnumerator animationVanish()
    {
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetBool("isVanishing", true);
        yield return new WaitForSeconds(3);


        portal.SetActive(true);

        ExperienceController exp = Instantiate(expObject, transform.position, transform.rotation);
        ExperienceController.experience = expValue;

        Destroy(gameObject);

        
    }


    IEnumerator DamageDisplay(int damage)
    {
        damageDisplay.text = "" + damage;
        yield return new WaitForSeconds(0.5f);
        damageDisplay.text = "";
    }

    IEnumerator floatDown()
    {

        rb.velocity = new Vector3(0, -1, 0);
        yield return new WaitForSeconds(2.5f);
        rb.velocity = new Vector3(-speed - 2, 0, 0);

    }
}
