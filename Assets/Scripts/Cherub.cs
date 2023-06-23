using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Cherub : MonoBehaviour
{
    // General Properties
    public GameObject bossProjectile;
    public int maxHealth;
    public int damage;
    public int expValue;
    public ExperienceController expObject;
    public GameObject portal;

    // Movement and Attacking properties
    public float speed;
    public float distance;
    public float moveDirection;

    // Booleans for different game phases and states
    private bool startup = true;
    private bool phase2Started = true;
    private bool phase2 = false;

    // Sprite properties
    private SpriteRenderer bossSprite;
    public static bool facingRight = true;
    public static bool patrol = false;

    // Contact with player
    private bool isTouchingPlayer = false;
    private float timeBetweenDmg;
    public float startTimeBetweenDmg;

    // Aggro and movement properties
    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    // Player properties
    public Rigidbody2D rb;
    public BoxCollider2D bc;


    // Animation and visual properties
    public Animator animator;
    public HealthBar healthBar;
    public Animator animFeedback;
    public Animator animFeedback2;
    public TMP_Text damageDisplay;
    public GameObject CanvasDamageNum;

    // Private properties for internal calculations
    private int currentHealth;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public Vector3 startPosition;
    private bool movingRight = true;
    public Transform groundDetection;

    // Magic numbers replaced with constants
    private const float xOffset = 5f;
    private const float yOffset = 2f;
    private const float damageDisplayDelay = 0.1f;
    private const float resetTriggerDelay = 0.2f;


    void Start()
    {
        bossSprite = GetComponent<SpriteRenderer>();
        bossSprite.flipX = true;
        currentHealth = maxHealth;
        HealthBar healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        rb.velocity = new Vector3(-speed, 1, 0);
        timeBetweenShots = startTimeBetweenShots;
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

        if (currentHealth <= 0)
        {
            StartCoroutine(animationVanish());
        }


        // Your phases can be more readable if organized into separate methods
        if (currentHealth >= 24000)
        {
            Phase1();
        }
        else if (currentHealth < 15000 && currentHealth > 1000)
        {
            Phase2();
        }
        else if (currentHealth < 1000)
        {
            Phase3();
        }
    }

    void Phase1()
    {
        Bounce();
    }

    void Phase2()
    {
        animator.SetBool("isShooting", true);
        Agressive();
    }

    void Phase3()
    {
        damage *= 10;
        animator.SetBool("isShooting", false);
        phase2 = true;
        rotateAroundMap();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        StartCoroutine(DamageDisplay(damage));

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        animator.SetBool("takingDamage", true);
        animFeedback.SetBool("takingDamage", true);
        animFeedback2.SetBool("takingDamage", true);

        StartCoroutine(ResetTakeDamageTrigger());
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
    private IEnumerator ResetTakeDamageTrigger()
    {
        yield return new WaitForSeconds(resetTriggerDelay);
        animator.SetBool("takingDamage", false);
        animFeedback.SetBool("takingDamage", false);
        animFeedback2.SetBool("takingDamage", false);
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
        Vector3 positionOffset = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset + Random.Range(1.0f, 3.0f), transform.position.z);
        GameObject text = Instantiate(CanvasDamageNum, positionOffset, Quaternion.identity);

        DamageNumController controller = text.GetComponentInChildren<DamageNumController>();

        controller.SetDamageNum(damage);
        yield return new WaitForSeconds(damageDisplayDelay);
    }

    IEnumerator floatDown()
    {

        rb.velocity = new Vector3(0, -1, 0);
        yield return new WaitForSeconds(2.5f);
        rb.velocity = new Vector3(-speed - 2, 0, 0);

    }
}
