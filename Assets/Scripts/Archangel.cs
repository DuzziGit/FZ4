using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Archangel : MonoBehaviour
{
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject bossProjectilePositive;
    public GameObject bossProjectileNegative;
    public GameObject bossProjectile;

    public int health;
    public int damage;

    public Seraphim Seraphim;

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
 
 public HealthBar healthBar;
    public int maxHealth;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public Animator animator;

    public TextMesh damageDisplay;

    public GameObject portal;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 60000;
        health = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        bossSprite = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        //bc = GetComponent<BoxCollider2D>();
        rb.velocity = new Vector3(-speed, 7, 0);
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
    void Update(){

        if (health <= 0)
        {
            StartCoroutine(animationTransform());
        } else
		{
            Agressive();
            Bounce();
        }

        healthBar.SetHealth(health);
        //chasePlayer();
    }

    public void TakeDamage(int damage)
    {
        animator.SetBool("Damage", true);
        StartCoroutine(animationDelay());
        StartCoroutine(DamageDisplay(damage));
        health -= damage;
        Debug.Log("Health: " + health);

    }

    void Agressive()
    {

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);

        shootPlayer();

        if (distanceToPlayer < agroRange)
        {
           chasePlayer();
            
        }

    }


    public void chasePlayer()
    {
        
        
        StartCoroutine(HoverPlayerX());

    }
    public void shootPlayer()
	{
        if (timeBetweenShots <= 0)
        {

            
            Instantiate(bossProjectile, transform.position, Quaternion.identity);
            Instantiate(bossProjectilePositive, transform.position, Quaternion.identity);
             Instantiate(bossProjectileNegative, transform.position, Quaternion.identity);

            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
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
        }
        else if (transform.position.x < startPosition.x)
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

            //Move Left
            yield return new WaitForSeconds(0.5f);
            Debug.Log("going left");
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
            bossSprite.flipX = true;
           

        }

        if (transform.position.x < GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x)
        {
            yield return new WaitForSeconds(0.5f);
            //Move Right
            Debug.Log("going right");
            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
            bossSprite.flipX = false;
            

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

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
                Debug.Log("Floor");
                rb.velocity = new Vector3(rb.velocity.x, 1, 0);
            }
        }
    }


    IEnumerator animationTransform()
    {
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetBool("isVanishing", true);
        yield return new WaitForSeconds(3);

        Instantiate(Seraphim, transform.position, transform.rotation);

        portal.SetActive(true);

        Destroy(gameObject);
    }


    IEnumerator DamageDisplay(int damage)
    {
        damageDisplay.text = "" + damage;
        yield return new WaitForSeconds(0.5f);
        damageDisplay.text = "";
    }


    IEnumerator animationDelay()
    {
        yield return new WaitForSeconds(0.10f);
        animator.SetBool("damage", false);
    }

}
