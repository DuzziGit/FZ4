using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skeleton : Enemy
{
    public AudioSource audioSource;
    public AudioClip skeletonHitSound;
    public int enemyDamage;
    public TMP_Text damageDisplay;
    public TMP_Text enemyLevel;
    public GameObject CanvasDamageNum;
    public bool isTouchingPlayer = false;
    private HealthBar healthBar; // Reference to the HealthBar script
    public Animator animFeedback;   // Declare animFeedback as an Animator
    public Animator animFeedback2;
    private int maxHealth; // Define the maxHealth variable

    void Start()
    {
        rb.velocity = new Vector3(speed, 0, 0);
        enemyLevel.text = level.ToString();
        enemyDamage = level * 5;

      /*  if (level > 0 && level < 10) TMP_Text.color = tutEnemy;
        else if (level > 10 && level < 20) TMP_Text.color = smallEnemy;
        else if (level > 20 && level < 30) TMP_Text.color = medEnemy;
        else if (level > 30 && level < 40) TMP_Text.color = bigEnemy;
*/
        animator = GetComponent<Animator>();

        healthBar = GetComponentInChildren<HealthBar>();

        maxHealth = health; // Set the maxHealth variable to the initial health value

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }
    void Update()
    {
      //  Debug.Log("max health" + maxHealth);
     //   Debug.Log("Current Health: " + health);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
      // Debug.Log("Damage Taken: " + damage);
        StartCoroutine(DamageDisplay(damage));
        // Damage calculation and other logic
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
       //     Debug.Log("max health" + maxHealth);
   //         Debug.Log("Current Health: " + health);
        }

        // Trigger the flashing animation
        animator.SetBool("takingDamage", true);
        animFeedback.SetBool("takingDamage", true);
        animFeedback2.SetBool("takingDamage", true);

        //  Debug.Log("taken damage ");

        // Delay the reset of the trigger parameter
        StartCoroutine(ResetTakeDamageTrigger());
        // audioSource.PlayOneShot(skeletonHitSound, 0.7f);
    }
    IEnumerator DamageDisplay(int damage)
    {
        float xOffset = 5f; 
        float yOffset = 2f; 
        Vector3 positionOffset = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset + Random.Range(1.0f, 3.0f), transform.position.z);
        GameObject text = Instantiate(CanvasDamageNum, positionOffset, Quaternion.identity);


        DamageNumController controller = text.GetComponentInChildren<DamageNumController>();
        
            controller.SetDamageNum(damage);
        
        // Wait a short time before instantiating the next damage number to make them stack
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator ResetTakeDamageTrigger()
    {
        // Wait for the flashing animation to complete
        yield return new WaitForSeconds(0.2f);

        // Reset the trigger parameter
        animator.SetBool("takingDamage", false);
        animFeedback.SetBool("takingDamage", false);
        animFeedback2.SetBool("takingDamage", false); 

    }
}