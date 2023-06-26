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
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

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
            StartCoroutine(AnimationVanish());
        }

        StartCoroutine(HoverPlayerX());
        HoverPlayerY();
        Agressive();

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
        StartCoroutine(HoverPlayerX());
        HoverPlayerY();
    }

    void Phase2()
    {
        animator.SetBool("isShooting", true);
        Bounce();
    }

    void Phase3()
    {
        damage *= 10;
        animator.SetBool("isShooting", false);
        phase2 = true;
        RotateAroundMap();
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

    void Agressive()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (timeBetweenShots <= 0)
        {
            Instantiate(bossProjectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots + 0.2f;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (distanceToPlayer < agroRange)
        {
            ChasePlayer();
        }
    }

    private IEnumerator ResetTakeDamageTrigger()
    {
        yield return new WaitForSeconds(resetTriggerDelay);
        animator.SetBool("takingDamage", false);
        animFeedback.SetBool("takingDamage", false);
        animFeedback2.SetBool("takingDamage", false);
    }

    private void ChasePlayer()
    {
        StartCoroutine(HoverPlayerX());
    }

    void RotateAroundMap()
    {
        if (phase2Started)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                rb.velocity = new Vector3(-speed - 2, 0, 0);
            }

            if (transform.position.x < playerTransform.position.x)
            {
                rb.velocity = new Vector3(speed + 2, 0, 0);
            }

            Debug.Log(startPosition);
            phase2Started = false;
        }
    }

    void GetStartPosition()
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
        // Return To Start Position
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
        float targetYPosition = playerTransform.position.y + 1f;
        float newYPosition = Mathf.Lerp(transform.position.y, targetYPosition, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    IEnumerator HoverPlayerX()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            yield return new WaitForSeconds(0.5f);
            float targetXPosition = playerTransform.position.x;
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
            bossSprite.flipX = true;

            while (transform.position.x > targetXPosition)
            {
                yield return null;
            }

            rb.velocity = Vector3.zero;
        }

        if (transform.position.x < playerTransform.position.x)
        {
            yield return new WaitForSeconds(0.5f);
            float targetXPosition = playerTransform.position.x;
            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
            bossSprite.flipX = false;

            while (transform.position.x < targetXPosition)
            {
                yield return null;
            }

            rb.velocity = Vector3.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy Touching Player");
            isTouchingPlayer = true;
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

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("Damage", false);
    }

    IEnumerator AnimationVanish()
    {
        rb.velocity = Vector3.zero;
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

}
