using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seraphim : MonoBehaviour {


    public int health;
    public int damage;
    public HealthBar healthBar;
    public int maxHealth;
    
    public int expValue;
    public ExperienceController expObject;
    public static bool isAggroed = false;

    public float speed;
    public float ySpeed;
    public float distance;

    private bool movingRight = true;


    public static bool facingRight = true;
    public static bool patrol = false;
    public float moveDirection;
    private SpriteRenderer bossSprite;

    public Vector3 startPosition;

    public float distanceFromStart;

    

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public Animator animator;

    public TextMesh damageDisplay;

    public bool startup;

    public bool isTouchingPlayer = false;
    public bool beginAttack = true;

    private float timeBetweenDmg;
    private float timeBetweenShot;
    private float timeBetweenTp;
    public float startTimeBetweenDmg;
    public float startTimeBetweenShot;
    public float startTimeBetweenTp;

    public Transform serphPos1;
    public Transform serphPos2;
    public Transform serphPos3;
    public Transform serphPosA;
    public Transform serphPosB;
    public Transform serphPosC;
    public Transform serphPosD;
    public Transform serphPosE;

    public Transform portalPosition;

    public GameObject serphFalling;
    public GameObject hubPortal;




    // Start is called before the first frame update
    void Start()
    {
        HealthBar healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        rb.velocity = new Vector3(0, -ySpeed, 0);
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
       
        animator.SetBool("isFlying", true);

        StartCoroutine(spawnDelay());
    }

    void FixedUpdate()
	{
        
    }



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);

       

        if (startup)
        {
            StartCoroutine(spawnDelay());
            startup = false;
        }

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

        if (health > 30000)
        {
            Bounce();
        } else

        if (health < 30000 && health > 0)
        {
            animator.SetBool("isFlying", true);
            rb.velocity = new Vector3(0, 0, 0);

            fallingAttack();
           
        } else

        if (health <= 0)
        {
            StartCoroutine(animationDeath());
        }

    }



    public void TakeDamage(int damage)
    {
        animator.SetBool("damage", true);
        StartCoroutine(animationDelay());
        StartCoroutine(DamageDisplay(damage));
        health -= damage;
        Debug.Log("Health: " + health);

    }

   

    void Bounce()
    {
        OnTriggerEnter2D(bc);
    }


    void fallingAttack()
	{
        
        animator.SetBool("isAttacking", true);
        if (timeBetweenShot <= 0)
        {
            StartCoroutine(serphAttack1());
            StartCoroutine(serphAttack2());
            StartCoroutine(serphAttack3());
            StartCoroutine(serphAttack4());
            StartCoroutine(serphAttack5());
            timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
            timeBetweenShot -= Time.deltaTime;
        }



        if (timeBetweenTp <= 0)
        {
            int random = Random.Range(1, 4);
            switch (random)
			{
                case 1:
                    transform.position = serphPos1.position;
                    break;

                case 2:
                    transform.position = serphPos2.position;
                    break;

                case 3:
                    transform.position = serphPos3.position;
                    break;

                default:
                break;
            }
            timeBetweenTp = startTimeBetweenTp;
        }
        else
        {
            timeBetweenTp -= Time.deltaTime;
        }




    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy Touching Player");
            isTouchingPlayer = true;
        }


        if (bossSprite.flipX == true)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("left wall");
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
                bossSprite.flipX = false;
            }
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("right wall");
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
            bossSprite.flipX = true;
        }

        if (collision.gameObject.tag == "Roof")
        {
            Debug.Log("Roof");
            beginAttack = true;
            rb.velocity = new Vector3(0, 0, 0);

        }
        if (collision.gameObject.tag == "World")
        {
            Debug.Log("Floor");
            animator.SetBool("isFlying", false);
            rb.velocity = new Vector3(speed, 0, 0);
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


    IEnumerator DamageDisplay(int damage)
    {
        damageDisplay.text = "" + damage;
        yield return new WaitForSeconds(0.5f);
        damageDisplay.text = "";
    }



    IEnumerator animationDeath()
    {
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetBool("isVanishing", true);

        yield return new WaitForSeconds(2);

        //Instantiate(hubPortal, portalPosition.position, portalPosition.rotation);
        hubPortal.SetActive(true);

        Destroy(gameObject);

        

    }

    IEnumerator animationDelay()
    {
        yield return new WaitForSeconds(0.9f);
        animator.SetBool("damage", false);
    }

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("isSpawning", false);
    }


   


    IEnumerator serphAttack1()
    {
        yield return new WaitForSeconds(1);
        Instantiate(serphFalling, serphPosA.position, serphPosA.rotation);

    }


    IEnumerator serphAttack2()
    {
        yield return new WaitForSeconds(2);
        Instantiate(serphFalling, serphPosB.position, serphPosB.rotation);

    }

    IEnumerator serphAttack3()
    {
        yield return new WaitForSeconds(3);

        Instantiate(serphFalling, serphPosC.position, serphPosC.rotation);

    }

    IEnumerator serphAttack4()
    {
        yield return new WaitForSeconds(4);
        Instantiate(serphFalling, serphPosD.position, serphPosD.rotation);

    }

    IEnumerator serphAttack5()
    {
        yield return new WaitForSeconds(5);

        Instantiate(serphFalling, serphPosE.position, serphPosE.rotation);

    }
}

