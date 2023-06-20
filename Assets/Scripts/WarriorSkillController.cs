using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WarriorSkillController : PlayerMovement
{

    public float MovementSpeedMultiplier;
    public float DashDistance;
    public GameObject SwordProjectile;
    public Animator animator;

    public LayerMask whatIsSolid;

    // audio source
    public AudioSource audiosource;
    public AudioClip DoubleSlashSoundEffect;
    public AudioClip WhirlWindSoundEffect;
    public AudioClip CarnageSoundEffect;
    public AudioClip DashSoundEffect;

    // Main skills
    private Skill DoubleSlash;
    private Skill WhirlWind;
    private Skill Shield;

    // Ultimate skill
    private Skill Carnage;

    // Movement skill
    private Skill Dash;



    [SerializeField]
    private Image imageCooldownS1;
    [SerializeField]
    private TMP_Text textCooldownS1;
     [SerializeField]
    private Image imageCooldownS2;
    [SerializeField]
    private TMP_Text textCooldownS2;
    [SerializeField]
    private Image imageCooldownS3;
    [SerializeField]
    private TMP_Text textCooldownS3;
    [SerializeField]
    private Image imageCooldownSM;
    [SerializeField]
    private TMP_Text textCooldownSM;
    [SerializeField]
    private Image imageCooldownSU;
    [SerializeField]
    private TMP_Text textCooldownSU;
       [SerializeField]
    private TMP_Text healthPotionText;

   public float cooldownTimeMovement = 2;
   private float nextFireTimeMovement = 0;

   public float cooldownTimeSkill1 = 2;
   private float nextFireTimeSkill1 = 0;

   public float cooldownTimeSkill2 = 2;
   private float nextFireTimeSkill2 = 0;

   public float cooldownTimeSkill3 = 2;
   private float nextFireTimeSkill3 = 0;

   public float cooldownTimeSkillUlt = 2;
   private float nextFireTimeSkillUlt = 0;


      public float cooldownTimeSkill3Upgraded;


    private float cooldownTimerS1 = 0.0f;
    private float cooldownTimerS2 = 0.0f;
    private float cooldownTimerS3 = 0.0f;
    private float cooldownTimerSM = 0.0f;
    private float cooldownTimerSU = 0.0f;
    private float cooldownTimer = 0.0f;
     public int damage1;
          public int damage2;
     public int damageUlt;
        public float distance;

        public int skillLevel1;
    public int skillLevel2;
    public int skillLevel3;
    public int skillUltLevel;


    // Start is called before the first frame update
    void Start()
    {
 Dash = new Skill("Dash", 0, 1); 

        // define all skills at start
        
        

        // grab audio source
        // audiosource = GetComponents<AudioSource>();

  maxHealth = level * 150;
        healthPotionValue = level * 10;
        currentHealth = maxHealth;
      //  healthBar.setMaxHealth(maxHealth);
              
       textCooldownS1.gameObject.SetActive(false);
      imageCooldownS1.fillAmount = 0.0f;
       textCooldownS2.gameObject.SetActive(false);
      imageCooldownS2.fillAmount = 0.0f;
       textCooldownS3.gameObject.SetActive(false);
      imageCooldownS3.fillAmount = 0.0f;
       textCooldownSM.gameObject.SetActive(false);
      imageCooldownSM.fillAmount = 0.0f;
       textCooldownSU.gameObject.SetActive(false);
      imageCooldownSU.fillAmount = 0.0f;
        currentExp = 0;

    }

    // Update is called once per frame
    void Update()
    {

                skillLevel1 = this.GetComponent<PlayerMovement>().skillOneLevel;
                skillLevel2 = this.GetComponent<PlayerMovement>().skillTwoLevel;
                skillLevel3 = this.GetComponent<PlayerMovement>().skillThreeLevel;
                skillUltLevel = this.GetComponent<PlayerMovement>().ultSkillLevel;



        damage1 = skillLevel1 * 45;
        damage2 = skillLevel2 * 10;
        damageUlt = skillUltLevel * 15;
       

    
        cooldownTimeSkill3Upgraded = cooldownTimeSkill3 - this.GetComponent<PlayerMovement>().skillThreeLevel;



 healthPotionText.text = healthPotions.ToString();
        experienceBar.setMaxExp(maxExp);
        levelUI.text = "" + level;
        maxHealth = level * 150;
        healthPotionValue = level * 20;


        maxExp = level * 23;


        //set the health bar to the current health of the player
        

        skillLevel1Text.text = skillOneLevel.ToString();
       skillLevel2Text.text = skillTwoLevel.ToString();
       skillLevel3Text.text = skillThreeLevel.ToString();
       skillUltText.text = ultSkillLevel.ToString();

       HealthDisplayText.text = "" + currentHealth.ToString() + " / " + maxHealth.ToString();
        maxExp = level * 23;
     //   healthBar.setMaxHealth(maxHealth);
    //    healthBar.SetHealth(currentHealth);

        experienceBar.SetExperience(currentExp);

     coinCount.text = coins.ToString();


//Get player inputs
        getPlayerInput();

        playerInteractInput();
        //Animate
        animate();

        //EnterPortal();
        LevelUp();

        if (Time.time > nextFireTimeSkill1) {

            // Double Slash
            if (Input.GetKeyDown(KeyCode.A))
            {

                // more or less the same as WhirlWind
                // add animation to the player somewhere in here to show the attack
                IEnumerator DoubleSlashSkill()
                {
                    int counter = 0;
                    while (counter <= 2)
                    {
                        // Raycast
                        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right, distance,whatIsSolid);
                        // draw raycast for debugging
                        Debug.DrawRay(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position,
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().right / 4, Color.red, 1f);
                        animator.SetBool("isUsingSlash", true);
                        // Check if it hit something
                        Debug.Log("CHECKING RASYCAST " + raycast.collider);
                        if (raycast.collider != null)
                        {
                              if (raycast.collider.CompareTag("Seraphim"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage1);
                    raycast.collider.GetComponent<Seraphim>().TakeDamage(damage1);
                }
                else

                if (raycast.collider.CompareTag("Archangel"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage1);
                    raycast.collider.GetComponent<Archangel>().TakeDamage(damage1);
                }
                else

                if (raycast.collider.CompareTag("Cherub"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage1);
                    raycast.collider.GetComponent<Cherub>().TakeDamage(damage1);
                }
                else

                            // Did the raycast hit an enemy?
                            if (raycast.collider.CompareTag("Skeleton")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage1); 
                       raycast.collider.GetComponent<Skeleton>().TakeDamage(damage1);
                       if(currentHealth < maxHealth){
                       }
                    }else if (raycast.collider.CompareTag("Slime")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage1); 
                      if(currentHealth < maxHealth){
                       }
                       raycast.collider.GetComponent<Slime>().TakeDamage(damage1);
                    }else if (raycast.collider.CompareTag("Bat")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage1); 
                    raycast.collider.GetComponent<Bat>().TakeDamage(damage1);
                    if(currentHealth < maxHealth){
                       }
                    }
                            else
                            {
                                Debug.Log("Ray didnt hit an enemy");
                            }
                        } 
                        else
                        {
                            Debug.Log("Ray didnt hit anything");
                        }
                        yield return new WaitForSeconds(0.1f);
                        counter += 1;
                        //Debug.Log("Double Slash");

                        // play sound
                        audiosource.PlayOneShot(DoubleSlashSoundEffect, 0.7f);

                    }
                    yield return null;
                    animator.SetBool("isUsingSlash", false);
                }
                StartCoroutine(DoubleSlashSkill());
                nextFireTimeSkill1 = Time.time + cooldownTimeSkill1;
                 textCooldownS1.gameObject.SetActive(true);
      cooldownTimerS1 = cooldownTimeSkill1;

            }
        }

        // WhirlWind
        if (Time.time > nextFireTimeSkill2)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {

                IEnumerator WhirlWindSkill()
                {
                    float duration = Time.time + 2.0f;
                    while (Time.time < duration)
                    {

                        // attack after every rotation of the player
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().Rotate(0f, 180f, 0f);
                        // Add a raycast in the direction of the player, see if it hits an enemy, if it does, damage it.

                        // Raycast
                        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right, distance,whatIsSolid);
                        // draw raycast for debugging
                        Debug.DrawRay(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position,
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().right / 4, Color.red, 1f);

                        // Check if it hit something
                        if (raycast.collider != null)
                        {

                                           if (raycast.collider.CompareTag("Seraphim"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage2);
                    raycast.collider.GetComponent<Seraphim>().TakeDamage(damage2);
                }
                else

                if (raycast.collider.CompareTag("Archangel"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage2);
                    raycast.collider.GetComponent<Archangel>().TakeDamage(damage2);
                }
                else

                if (raycast.collider.CompareTag("Cherub"))
                {
                    Debug.Log("ENEMY MUST TAKE DAMAGE !" + damage2);
                    raycast.collider.GetComponent<Cherub>().TakeDamage(damage2);
                }
                else
                            // Did the raycast hit an enemy?
                                // Did the raycast hit an enemy?
                            if (raycast.collider.CompareTag("Skeleton")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage2); 
                       raycast.collider.GetComponent<Skeleton>().TakeDamage(damage2);
                    }else if (raycast.collider.CompareTag("Slime")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage2); 
                       raycast.collider.GetComponent<Slime>().TakeDamage(damage2);
                    }else if (raycast.collider.CompareTag("Bat")) {
                      Debug.Log("ENEMY MUST TAKE DAMAGE !"  + damage2); 
                    raycast.collider.GetComponent<Bat>().TakeDamage(damage2);
                    }
                        }
                        // play sound
                        audiosource.PlayOneShot(WhirlWindSoundEffect, 0.7f);

                        yield return new WaitForSeconds(0.2F);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().Rotate(0f, 0f, 0f);
                    }
                    //reset direction at the end of the skill
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().Rotate(0f, 0f, 0f);


                    yield return null;
                }
                StartCoroutine(WhirlWindSkill());
                nextFireTimeSkill2 = Time.time + cooldownTimeSkill2;
                 textCooldownS2.gameObject.SetActive(true);
                 cooldownTimerS2 = cooldownTimeSkill2;
            }
        }

        // Shield
        if (Time.time > nextFireTimeSkill3) 
        {

            if (Input.GetKeyDown(KeyCode.D))
            {

                // this will need to be verified and edited onces enemies and damage are implemented, but it should work
                IEnumerator ShieldSkill()
                {
               

                   currentHealth += level * 10;
                //     healthBar.setMaxHealth(maxHealth);
                //     healthBar.SetHealth(currentHealth);
                       yield return new WaitForSeconds(4.5F);


                    currentHealth = maxHealth;
                 //      healthBar.setMaxHealth(maxHealth);
                //     healthBar.SetHealth(currentHealth);
                    yield return null;

                }
                StartCoroutine(ShieldSkill());
                nextFireTimeSkill3 = Time.time + cooldownTimeSkill3Upgraded;
                    textCooldownS3.gameObject.SetActive(true);
      cooldownTimerS3 = cooldownTimeSkill3Upgraded;
            }
        }
        
        if (Time.time > nextFireTimeMovement) 
        {

            // Dash
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {

                Debug.Log("Dash key pressed");
                IEnumerator DashSkill()
                {
                    // set soem sort of base speed to return to after the dash
                    float basespeed = 1.75f;
                    // breifly increase the players movement speed to simulate a dash
                    moveSpeed = Dash.Movement(5f);
                    // play sound
                    audiosource.PlayOneShot(DashSoundEffect, 0.7f);
                    yield return new WaitForSeconds(0.2F);
                    // reset the players movement speed
                    moveSpeed = basespeed;

                }
                StartCoroutine(DashSkill());
                nextFireTimeMovement = Time.time + cooldownTimeMovement;
  textCooldownSM.gameObject.SetActive(true);
                                cooldownTimerSM = cooldownTimeMovement; 
            }
        }

        // Carnage
        if (Time.time > nextFireTimeSkillUlt) {

            if (Input.GetKeyDown(KeyCode.F))
            {
                       

                IEnumerator CarnageSkill()
                {
                    // Get the players current position
                    Vector3 playerCurrentTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

                    // set how high the swords fall by adding some offset to the players current y value
                    playerCurrentTransform.y += 1f;

                    // This is the spacing between the projectiles
                    float distance = 0.2f;
                    float currentRotation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().rotation.y;
                    if (currentRotation >= 0)
                    {
                        while (distance < 2.2f)
                        {
                            // spawn a sword at the current distance 
                            Instantiate(SwordProjectile, new Vector3(playerCurrentTransform.x + distance, playerCurrentTransform.y, playerCurrentTransform.z),
                                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().rotation);

                            // add some more distance between the projectiles
                            distance += 0.2f;

                            // play sound
                            audiosource.PlayOneShot(CarnageSoundEffect, 0.7f);

                            // wait some times
                            yield return new WaitForSeconds(0.2F);
                        }
                    }
                     else
                     {
                         while (distance < 2.2f)
                        {
                            // spawn a sword at the current distance 
                            Instantiate(SwordProjectile, new Vector3(playerCurrentTransform.x - distance, playerCurrentTransform.y, playerCurrentTransform.z),
                                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().rotation);

                            // add some more distance between the projectiles
                            distance += 0.2f;

                            // play sound
                            audiosource.PlayOneShot(CarnageSoundEffect, 0.7f);

                            // wait some times
                            yield return new WaitForSeconds(0.2F);
                        }
                    }

                    yield return null;
                }
                StartCoroutine(CarnageSkill());
                nextFireTimeSkillUlt = Time.time + cooldownTimeSkillUlt;
                 textCooldownSU.gameObject.SetActive(true);
      cooldownTimerSU = cooldownTimeSkillUlt;
            }
        }
                ApplyCooldownTracker();

    }
    private void FixedUpdate() {
    OnTriggerEnter2D(bc);
    //Move Player
    moveCharacter();
  }
    public void ApplyCooldownTracker()
  {
    cooldownTimerS1 -= Time.deltaTime;
    cooldownTimerS2 -= Time.deltaTime;
    cooldownTimerS3 -= Time.deltaTime;
    cooldownTimerSM -= Time.deltaTime;
    cooldownTimerSU -= Time.deltaTime;

    if (cooldownTimerS1 < 0.0f) {
      textCooldownS1.gameObject.SetActive(false);
      imageCooldownS1.fillAmount = 0.0f;

       
    } else {
 textCooldownS1.text = Mathf.RoundToInt(cooldownTimerS1).ToString();
      imageCooldownS1.fillAmount = cooldownTimerS1 / cooldownTimeSkill1;
    }
    
    
     if (cooldownTimerS2 < 0.0f) {
      textCooldownS2.gameObject.SetActive(false);
      imageCooldownS2.fillAmount = 0.0f;
    }  else {
 textCooldownS2.text = Mathf.RoundToInt(cooldownTimerS2).ToString();
      imageCooldownS2.fillAmount = cooldownTimerS2 / cooldownTimeSkill2;
    }
    
     if (cooldownTimerS3 < 0.0f) {
      textCooldownS3.gameObject.SetActive(false);
      imageCooldownS3.fillAmount = 0.0f;
    }  else {
 textCooldownS3.text = Mathf.RoundToInt(cooldownTimerS3).ToString();
      imageCooldownS3.fillAmount = cooldownTimerS3 / cooldownTimeSkill3Upgraded;
    }

     if (cooldownTimerSU < 0.0f) {
      textCooldownSU.gameObject.SetActive(false);
      imageCooldownSU.fillAmount = 0.0f;
    }  else {
 textCooldownSU.text = Mathf.RoundToInt(cooldownTimerSU).ToString();
      imageCooldownSU.fillAmount = cooldownTimerSU / cooldownTimeSkillUlt;
    }
     if (cooldownTimerSM < 0.0f) {
      textCooldownSM.gameObject.SetActive(false);
      imageCooldownSM.fillAmount = 0.0f;
    }  else {
 textCooldownSM.text = Mathf.RoundToInt(cooldownTimerSM).ToString();
      imageCooldownSM.fillAmount = cooldownTimerSM / cooldownTimeMovement;
    }
    
  }
}
